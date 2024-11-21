using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosVoluntario;
using Application.Specifications.MiPortal.RetirosAhorroVoluntario;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Shared.Services
{
    public class RetiroAhorroVoluntarioService : IRetiroAhorroVoluntarioService
    {
        private readonly IFilesManagerService _filesManagerService;
        private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryRetiroAhorroVoluntario;
        private readonly IRepositoryAsync<Employee> _repositoryEmployee;
        private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAhorroVoluntario;
        private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsyncMovimientoAhorroVoluntario;
        private readonly ISendMailService _sendMailService;
        private readonly IPeriodosService _periodosService;
        private readonly IMonedaService _monedaService;

        public RetiroAhorroVoluntarioService(IFilesManagerService filesManagerService, IRepositoryAsync<RetiroAhorroVoluntario> repositoryRetiroAhorroVoluntario, IRepositoryAsync<Employee> repositoryEmployee, IRepositoryAsync<AhorroVoluntario> repositoryAhorroVoluntario, ISendMailService sendMailService, IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsyncMovimientoAhorroVoluntario, IPeriodosService periodosService, IMonedaService monedaService)
        {
            _filesManagerService = filesManagerService;
            _repositoryRetiroAhorroVoluntario = repositoryRetiroAhorroVoluntario;
            _repositoryEmployee = repositoryEmployee;
            _repositoryAhorroVoluntario = repositoryAhorroVoluntario;
            _sendMailService = sendMailService;
            _repositoryAsyncMovimientoAhorroVoluntario = repositoryAsyncMovimientoAhorroVoluntario;
            _periodosService = periodosService;
            _monedaService = monedaService;
        }


        public string SaveSolicitudFirmada(IFormFile file_solicitud_firmada, int retiro_ahorro_id,int ahorro_id)
        {
            return _filesManagerService.saveFileInTo(file_solicitud_firmada, (retiro_ahorro_id*10000)+ahorro_id, "StaticFiles\\Mate", "FormatosPDF\\AhorrosVoluntario\\RetirosAhorroVoluntario\\SolicitudesFirmados", ".pdf");
        }

        public string SaveConstanciaTransferenciaPDF(IFormFile file_contancia_transferencia, int retiro_ahorro_id, int ahorro_id)
        {
            return _filesManagerService.saveFileInTo(file_contancia_transferencia, (retiro_ahorro_id * 10000) + ahorro_id, "StaticFiles\\Mate", "FormatosPDF\\AhorrosVoluntario\\RetirosAhorroVoluntario\\ConstanciasTransferencia", ".pdf");
        }

        public string SaveConstanciaPagoPDF(IFormFile file_contancia_pago, int retiro_ahorro_id, int ahorro_id)
        {
            return _filesManagerService.saveFileInTo(file_contancia_pago, (retiro_ahorro_id * 10000) + ahorro_id, "StaticFiles\\Mate", "FormatosPDF\\AhorrosVoluntario\\RetirosAhorroVoluntario\\ConstanciasPago", ".pdf");
        }

        public async Task<bool> AddMovimientoRetiroAhorroVoluntario(RetiroAhorroVoluntario retiro_ahorro_voluntario, AhorroVoluntario ahorro_voluntario)
        {
           
            if (retiro_ahorro_voluntario == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {retiro_ahorro_voluntario.Id}");
            }

            if (ahorro_voluntario == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {ahorro_voluntario.Id}");
            }
            var list = await _repositoryAsyncMovimientoAhorroVoluntario.ListAsync(new MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdSpecification(ahorro_voluntario.CompanyId, ahorro_voluntario.EmployeeId, ahorro_voluntario.Id));

            // Calcular el siguiente MovimientoId
            var ultimoMovimientoId = list.Any() ? list.Max(m => m.MovimientoId) : 0;
            var siguienteMovimientoId = ultimoMovimientoId + 1;

            var periodo = 0;
            if(DateTime.Now.Day > 15)
            {
                periodo = DateTime.Now.Year * 100 + DateTime.Now.Month * 2;
            } else
                periodo = DateTime.Now.Year * 100 + DateTime.Now.Month * 2 - 1;

            MovimientoAhorroVoluntario mov_ahorro_voluntario = new MovimientoAhorroVoluntario();
            try
            {
                mov_ahorro_voluntario.AhorroVoluntarioId = ahorro_voluntario.Id;
                mov_ahorro_voluntario.EmployeeId = ahorro_voluntario.EmployeeId;
                mov_ahorro_voluntario.CompanyId = ahorro_voluntario.CompanyId;
                mov_ahorro_voluntario.MovimientoId = siguienteMovimientoId;
                mov_ahorro_voluntario.Periodo = periodo;
                mov_ahorro_voluntario.Monto = ((float)retiro_ahorro_voluntario.Cantidad) * (-1); // negativo al ser un retiro
                mov_ahorro_voluntario.Rendimiento = 0;
                mov_ahorro_voluntario.EstadoTransaccion = EstadoTransaccion.Exitoso;
                mov_ahorro_voluntario.Interes = 0;

                await _repositoryAsyncMovimientoAhorroVoluntario.AddAsync(mov_ahorro_voluntario);

            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error al crear el movimiento de retiro: {e.Message}");
            }

            return true;

        }

        public async Task<bool> EnviarCorreosTransferencia(RetiroAhorroVoluntario retiro_ahorro_voluntario, AhorroVoluntario ahorro_voluntario)
        {
            Employee employee = await _repositoryEmployee.GetByIdAsync(ahorro_voluntario.EmployeeId);

            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarSolicitudPlanes.html")).ToString();

            mailHTML = mailHTML.Replace("#_TITULO_#", "Ahorro Voluntario");
            mailHTML = mailHTML.Replace("#_SUBTITULO_#", "Transferencia de pago");
            mailHTML = mailHTML.Replace("#_SECCION1_#", $"Estimado(a) {employee.NombreCompletoOrdenado()}, se le ha realizado una tranferencia por ${retiro_ahorro_voluntario.Cantidad.ToString("0.00")}.");

            if (employee == null)
            {
                throw new ApiException($"Empleado con Id {ahorro_voluntario.EmployeeId} no existe.");
            }

            var correo = employee.MailCorporativo;

            if (correo == null || correo.Equals(""))
            {
                throw new ApiException($"El empleado no cuenta con correo registrado.");
            }

            string[] lista_correos = {
                    correo,
                    "erika.aldama@solucionesintegrales-mex.com.mx",
                    "arturo.pazgo@gmail.com"
            };

            try
            {
                _sendMailService.SendEmailWithAttachment("facturacion@maxal.com.mx", lista_correos, "Transferencia de pago de retiro de ahorro voluntarios", mailHTML, null);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return true;


        }

        public async Task<bool> EnviarCorreoEstatus(RetiroAhorroVoluntario retiro_ahorro_voluntario, AhorroVoluntario ahorro_voluntario)
        {
            Employee employee = await _repositoryEmployee.GetByIdAsync(ahorro_voluntario.EmployeeId);

            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarSolicitudPlanes.html")).ToString();

            mailHTML = mailHTML.Replace("#_TITULO_#", "Ahorro Voluntario");
            mailHTML = mailHTML.Replace("#_SUBTITULO_#", "Estatus de operación");

            if (retiro_ahorro_voluntario.Estatus == EstatusRetiro.Pendiente)
            {
                mailHTML = mailHTML.Replace("#_SECCION1_#", $"El empleado(a)  {employee.NombreCompletoOrdenado()}, ha solicitado un retiro de su ahorro voluntario.");
            }
            else
            {
                mailHTML = mailHTML.Replace("#_SECCION1_#", $"Estimado(a) {employee.NombreCompletoOrdenado()}, su retiro a sido  {retiro_ahorro_voluntario.Estatus.ToString()}.");
            }

            if (employee == null)
            {
                throw new ApiException($"Empleado con Id {ahorro_voluntario.EmployeeId} no existe.");
            }

            var correo = employee.MailCorporativo;

            if (correo == null || correo.Equals(""))
            {
                throw new ApiException($"El empleado no cuenta con correo registrado.");
            }

            string[] lista_correos = {
                    correo,
                    "erika.aldama@solucionesintegrales-mex.com.mx",
                    "arturo.pazgo@gmail.com"
            };

            try
            {
                _sendMailService.SendEmailWithAttachment("facturacion@maxal.com.mx", lista_correos, " Estatus de retiro de ahorro voluntario", mailHTML, null);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return true;


        }


        public async Task<string> SolicitudRetiroPDF(int retiro_id,int ahorro_id)
        {
            RetiroAhorroVoluntario retiro_ahorro_voluntario = await _repositoryRetiroAhorroVoluntario.GetBySpecAsync(new RetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdSpecification(retiro_id, ahorro_id));
            AhorroVoluntario ahorro_voluntario = await _repositoryAhorroVoluntario.GetByIdAsync(ahorro_id);
            Employee employee = await _repositoryEmployee.GetByIdAsync(ahorro_voluntario.EmployeeId);
            var nombre = employee.NombreCompletoOrdenado().ToUpper();

            var document = Document.Create(container =>
            {


                container.Page(page =>
                {
                    page.Margin(1);

                    //page.Header().Height(100).Background(Colors.Grey.Lighten1);

                    page.Header().Height(120).Border(0).Layers(layers =>
                    {
                        DateTime dateTime = DateTime.Now;
                        layers.PrimaryLayer().Border(0);

                        layers.Layer().TranslateX(75).TranslateY(95).Text("AHORRO VOLUNTARIO").FontSize(10).Bold().FontFamily(Fonts.TimesNewRoman);

                        byte[] imageLogo = File.ReadAllBytes("C:\\StaticFiles\\Mate\\img\\wise_LOGO.png");
                        layers.Layer().Width(180).Border(0).TranslateX(30).TranslateY(50).Image(imageLogo);

                        layers.Layer().Height(60).Width(250).TranslateX(310).TranslateY(75).Border(0).AlignRight().Text(text =>
                        {
                            text.Line("CDMX a " + dateTime.ToString("dd 'de' MMMM 'del' yyyy.")).FontSize(10).Bold();
                            //text.Line("Oficina: Ciudad de México.").FontSize(10).Bold();
                            //text.Line("Folio:   " + 9998).FontSize(10).Bold();
                        });


                    });

                    //.Background(Colors.Grey.Lighten3)
                    page.Content().PaddingVertical(40).Column(column =>
                    {

                        column.Spacing(10);
                        column.Item().TranslateX(30).TranslateY(0).Width(530).Border(0).AlignCenter().Column(column =>
                        {
                            column.Item().Text("Solicitud de Entrega de Ahorro").FontSize(18).Bold().FontFamily(Fonts.Arial).LineHeight(2);
                        });



                        column.Item().TranslateX(30).TranslateY(0).Width(530).PaddingTop(30).Border(0).Column(column =>
                        {
                            column.Item().Text($"En mi propio derecho y de acuerdo a los artículos 21 y 22 del Reglamento de Administración de Plan Complementario " +
                                $"de Beneficios WISE, yo {nombre}, por medio del presente, solicito la cantidad de $ {retiro_ahorro_voluntario.Cantidad} " +
                                $"({_monedaService.ConvertNumberToString(retiro_ahorro_voluntario.Cantidad)}), procedentes de mi subcuenta de Ahorro Voluntario.")
                                .FontSize(12).FontFamily(Fonts.Arial);
                        });


                        column.Item().TranslateY(0).AlignCenter().Row(row =>
                        {
                            row.Spacing(50);

                            row.AutoItem().AlignCenter().PaddingTop(60).Column(column =>
                            {

                                column.Item().AlignCenter().Text("Atentamente").FontSize(12).FontFamily(Fonts.Arial);
                                column.Item().Width(300).PaddingTop(20).LineHorizontal(1).LineColor(Colors.Black);

                                column.Item().AlignCenter().Text(nombre).FontSize(12).FontFamily(Fonts.Arial); // jefe directo


                            });

                        });
                    });

                    page.Footer().Height(50).Border(0).AlignCenter().Padding(10).Text(text =>
                    {
                        text.Line("Cerro del Borrego 107 , Int. 6, Campestre Churubusco C.P. 04200, Coyoacán, CDMX").FontSize(10).FontFamily(Fonts.Verdana).FontColor("#808080").Bold();


                    });
                });
            });

            string nombrePDF = "SolicitudRetiro-" + retiro_id +"-"+ ahorro_id;
            var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\AhorrosVoluntario\RetirosAhorroVoluntario\PDFsTemporales", $"{nombrePDF}.pdf");
            document.GeneratePdf(rutaCompleta);
            return rutaCompleta.Substring(3);

        }


        public async Task<string > ConstanciaTransferenciaPDF(int retiro_id, int ahorro_id)
        {
            RetiroAhorroVoluntario retiro_ahorro_voluntario = await _repositoryRetiroAhorroVoluntario.GetBySpecAsync(new RetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdSpecification(retiro_id, ahorro_id));
            AhorroVoluntario ahorro_voluntario = await _repositoryAhorroVoluntario.GetByIdAsync(ahorro_id);
            Employee employee = await _repositoryEmployee.GetByIdAsync(ahorro_voluntario.EmployeeId);
            var nombre = employee.NombreCompletoOrdenado().ToUpper();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(1);

                    //page.Header().Height(100).Background(Colors.Grey.Lighten1);

                    page.Header().Height(150).Border(0).Layers(layers =>
                    {

                        layers.PrimaryLayer().Border(0);

                        byte[] imageLogo2 = File.ReadAllBytes("C:\\StaticFiles\\Mate\\img\\logo_wise1.png");
                        layers.Layer().Width(230).Border(0).TranslateX(30).TranslateY(60).Image(imageLogo2);

                        DateTime dateTime = DateTime.Now;
                        layers.Layer().Height(60).Width(250).TranslateX(310).TranslateY(100).Border(0).AlignRight().Text(text =>
                        {
                            text.Line("CDMX a " + dateTime.ToString("dd 'de' MMMM 'del' yyyy.")).FontSize(10).Bold();
                        });
                    });

                    //.Background(Colors.Grey.Lighten3)
                    page.Content().Border(0).Column(column =>
                    {

                        // Contenido
                        column.Item().PaddingTop(10).AlignCenter().Width(560).Border(0).Column(column =>
                        {
                            column.Spacing(10);
                            column.Item().TranslateX(30).TranslateY(0).Width(530).Border(0).AlignCenter().Column(column =>
                            {
                                column.Item().Text("Solicitud de Entrega de Ahorro").FontSize(18).Bold().FontFamily(Fonts.Arial).LineHeight(2);
                            });



                            column.Item().TranslateX(30).TranslateY(0).Width(530).PaddingTop(30).Border(0).Column(column =>
                            {
                                column.Item().Text($"Por medio de la presente, hago constar que yo, {nombre}, recibí la" +
                                    $" cantidad de ${retiro_ahorro_voluntario.Cantidad} ({_monedaService.ConvertNumberToString(retiro_ahorro_voluntario.Cantidad)})," +
                                    $" procedente del total de mi subcuenta de Ahorro Voluntario" +
                                    $" más interés generado.")
                                    .FontSize(12).FontFamily(Fonts.Arial);

                            });

                            column.Item().TranslateX(30).TranslateY(0).Width(530).PaddingTop(30).Border(0).Column(column =>
                            {
                                column.Item().Text($"Sirva el presente documento como respaldo de la transacción y manifiesto de conformidad al respecto, " +
                                    $"deslindando de cualquier responsabilidad legal a la empresa Consulting Integrated to Planning and Entrepreneurship (CIPE).")
                                .FontSize(12).FontFamily(Fonts.Arial);
                            });

                            column.Item().TranslateX(30).TranslateY(0).Width(530).PaddingTop(30).Border(0).Column(column =>
                            {
                                column.Item().Text($"Además, hago constar que es mi elección:").FontSize(12).FontFamily(Fonts.Arial);


                                column.Item().Row(row =>
                                {
                                    if (retiro_ahorro_voluntario.SeguirAhorrando)
                                    {
                                        row.ConstantItem(20).Border(1).AlignCenter().Text("X").FontSize(12).FontFamily(Fonts.Arial);
                                    }
                                    else
                                    { 
                                        row.ConstantItem(20).Border(1).AlignCenter().Text("").FontSize(12).FontFamily(Fonts.Arial);
                                    }
                                    row.RelativeItem().PaddingLeft(8).AlignMiddle().Text("Continuar ahorrando.").FontSize(12).FontFamily(Fonts.Arial);
                                });

                                column.Item().Row(row =>
                                {
                                    if (!retiro_ahorro_voluntario.SeguirAhorrando)
                                    {
                                        row.ConstantItem(20).Border(1).AlignCenter().Text("X").FontSize(12).FontFamily(Fonts.Arial);
                                    }
                                    else
                                    {
                                        row.ConstantItem(20).Border(1).AlignCenter().Text("").FontSize(12).FontFamily(Fonts.Arial);
                                    }
                                    row.RelativeItem().PaddingLeft(8).AlignMiddle().Text("Cancelación de ahorro.").FontSize(12).FontFamily(Fonts.Arial);
                                });

                            });



                            column.Item().TranslateY(0).AlignCenter().Row(row => {
                                row.Spacing(50);

                                row.AutoItem().AlignCenter().PaddingTop(60).Column(column => {

                                    column.Item().AlignCenter().Text("Atentamente").FontSize(12).FontFamily(Fonts.Arial);
                                    column.Item().Width(200).PaddingTop(40).LineHorizontal(1).LineColor(Colors.Black);

                                    column.Item().AlignCenter().Text(nombre).FontSize(12).FontFamily(Fonts.Arial); // jefe directo


                                });

                            });

                            column.Item().TranslateX(30).TranslateY(0).Width(530).PaddingTop(100).Border(0).Column(column =>
                            {
                                column.Item().Text($"Notas: Se adjunta copia de identificación oficial para la constancia. Favor de escribir con tinta azul" +
                                    $" nombre, firma y fecha.")
                                .FontSize(9).FontFamily(Fonts.Arial);
                            });
                        });
                    });

                    page.Footer().Height(50).Border(0).AlignCenter().Padding(10).Column(column =>
                    {

                    });
                });
            });


            string nombrePDF = "ConstanciaTransferencia-" + retiro_id + "-" + ahorro_id;
            var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\AhorrosVoluntario\RetirosAhorroVoluntario\PDFsTemporales", $"{nombrePDF}.pdf");
            document.GeneratePdf(rutaCompleta);
            return rutaCompleta.Substring(3);

        }


    }
}
