using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Application.Exceptions;
using Application.Wrappers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Domain.Enums;

namespace Shared.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IFilesManagerService _filesManagerService;
        private readonly ISendMailService _sendMailService;
        public PrestamoService(IRepositoryAsync<Prestamo> repositoryAsyncPrestamo, IFilesManagerService filesManagerService, IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<Company> repositoryAsyncCompany, ISendMailService sendMailService)
        {
            _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
            _filesManagerService = filesManagerService;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _sendMailService = sendMailService;
        }

        public void SetFechaAndEstatus(Prestamo prestamo,EstatusOperacion estatus)
        {
            prestamo.Estatus = estatus;
            switch(estatus)
            {
                case EstatusOperacion.Pendiente:
                    prestamo.FechaEstatusPendiente = DateTime.Now;
                    break;

                case EstatusOperacion.Activo:
                    prestamo.FechaEstatusActivo = DateTime.Now;
                    break;

                case EstatusOperacion.Finiquitado:
                    prestamo.FechaEstatusFiniquitado = DateTime.Now;
                    break;

                case EstatusOperacion.Rechazado:
                    prestamo.FechaEstatusRechazado = DateTime.Now;
                    break;

                default: 
                    break;
            }
        }

        public string SaveAcuseFirmadoPDF(IFormFile file, int id)
        {
            return _filesManagerService.saveFileInTo(file, id, "StaticFiles\\Mate", "FormatosPDF\\Prestamos\\AcusesFirmado", ".pdf");
        }

        public string SavePagarePDF(IFormFile file, int id)
        {
            return _filesManagerService.saveFileInTo(file, id, "StaticFiles\\Mate", "FormatosPDF\\Prestamos\\Pagares", ".pdf");
        }

        public string SaveConstanciaRetiroPDF(IFormFile file, int id)
        {
            return _filesManagerService.saveFileInTo(file, id, "StaticFiles\\Mate", "FormatosPDF\\Prestamos\\ConstanciasRetiro", ".pdf");
        }

        public string SaveConstanciaTransferenciaPDF(IFormFile file, int id)
        {
            return _filesManagerService.saveFileInTo(file, id, "StaticFiles\\Mate", "FormatosPDF\\Prestamos\\ConstanciasTransferencia", ".pdf");

        }

        public async Task<bool> EnviarCorreosTransferencia(int prestamo_id)
        {
            Prestamo prestamo = await _repositoryAsyncPrestamo.GetByIdAsync(prestamo_id);
            Employee employee = await _repositoryAsyncEmployee.GetByIdAsync(prestamo.EmployeeId);

            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarSolicitudPlanes.html")).ToString();

            mailHTML = mailHTML.Replace("#_TITULO_#", "Prestamo");
            mailHTML = mailHTML.Replace("#_SUBTITULO_#", "Transferencia de pago");
            mailHTML = mailHTML.Replace("#_SECCION1_#", $"Se ha realizado una tranferencia de pago a {employee.NombreCompletoOrdenado()} por ${prestamo.Monto.ToString("0.00")}");

            if (employee == null)
            {
                throw new ApiException($"Empleado con Id {prestamo.EmployeeId} no existe.");
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
                _sendMailService.SendEmailWithAttachment("facturacion@maxal.com.mx", lista_correos, "Transeferencia de pago a prestamo", mailHTML, null);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return true;


        }

        public async Task<bool> EnviarCorreoEstatus(Prestamo prestamo)
        {
            Employee employee = await _repositoryAsyncEmployee.GetByIdAsync(prestamo.EmployeeId);

            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarSolicitudPlanes.html")).ToString();

            string estatus = prestamo.Estatus.ToString();

            if (prestamo.Estatus == EstatusOperacion.Activo)
            {
                estatus = "Autorizado";
            }

            mailHTML = mailHTML.Replace("#_TITULO_#", "Prestamo");
            mailHTML = mailHTML.Replace("#_SUBTITULO_#", "Estatus de operación");
            if (prestamo.Estatus == EstatusOperacion.Pendiente)
            {
                mailHTML = mailHTML.Replace("#_SECCION1_#", $"El empleado(a) {employee.NombreCompletoOrdenado()}, ha solicitado un prestamo.");
            } else
            {
                mailHTML = mailHTML.Replace("#_SECCION1_#", $"Estimado(a) {employee.NombreCompletoOrdenado()}, su prestamo a sido  {estatus}.");
            }


            if (employee == null)
            {
                throw new ApiException($"Empleado con Id {prestamo.EmployeeId} no existe.");
            }

            var correo = employee.MailCorporativo;

            if (string.IsNullOrEmpty(correo))
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
                _sendMailService.SendEmailWithAttachment("facturacion@maxal.com.mx", lista_correos, "Estatus de prestamo", mailHTML, null);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return true;


        }

        public async Task<string> AcusePDF(int prestamo_id)
        {

            Prestamo prestamo = await _repositoryAsyncPrestamo.GetByIdAsync(prestamo_id);
            Employee employee = await _repositoryAsyncEmployee.GetByIdAsync(prestamo.EmployeeId);

            var folio = prestamo.Id;
            var cantidad = prestamo.Monto;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(1);

                    //page.Header().Height(100).Background(Colors.Grey.Lighten1);
                    page.Header().Height(150).Layers(layers =>
                    {
                        DateTime dateTime = DateTime.Now;
                        layers.PrimaryLayer().Border(0);

                        layers.Layer().TranslateX(85).TranslateY(90).Text("PRÉSTAMOS").FontSize(10).Bold().FontFamily(Fonts.TimesNewRoman);

                        byte[] imageLogo = File.ReadAllBytes("C:\\StaticFiles\\Mate\\img\\wise_LOGO.png");
                        layers.Layer().Width(2, Unit.Inch).TranslateX(30).TranslateY(50).Image(imageLogo);

                        layers.Layer().Height(60).Width(250).TranslateX(310).TranslateY(100).Border(0).AlignRight().Text(text =>
                        {
                            text.Line("CDMX a " + dateTime.ToString("dd 'de' MMMM 'del' yyyy.")).FontSize(10).Bold();
                            text.Line("Oficina: Ciudad de México.").FontSize(10).Bold();
                            text.Line("Folio:   " + 9998).FontSize(10).Bold();
                        });


                    });

                    page.Content().PaddingVertical(40).Column(column =>
                    {

                        column.Spacing(10);
                        column.Item().TranslateX(30).TranslateY(0).Width(530).Border(0).AlignCenter().Column(column =>
                        {
                            column.Item().Text("ACUSE DE AUTORIZACIÓN").FontSize(18).Bold().FontFamily(Fonts.Arial).LineHeight(2);
                        });

                        column.Item().TranslateX(30).TranslateY(0).Width(530).Border(0).Column(column =>
                        {
                            column.Item().Text("A quien corresponda: ")
                                .FontSize(12).FontFamily(Fonts.Arial);
                        });

                        column.Item().TranslateX(30).TranslateY(0).Width(530).PaddingTop(30).Border(0).Column(column =>
                        {
                            column.Item().Text($"Por medio de la presente doy fe que presenté una solicitud de préstamo con folio {folio} por la" +
                                $" cantidad de ${cantidad} a través de mi portal de Admin Xpert, por lo que autorizo el inicio del trámite.")
                                .FontSize(12).FontFamily(Fonts.Arial);
                        });


                        column.Item().TranslateY(0).AlignCenter().Row(row => {
                            row.Spacing(50);

                            row.AutoItem().AlignCenter().PaddingTop(60).Column(column => {

                                column.Item().AlignCenter().Text("Atentamente").FontSize(12).FontFamily(Fonts.Arial);
                                column.Item().Width(200).PaddingTop(20).LineHorizontal(1).LineColor(Colors.Black);

                                column.Item().AlignCenter().Text(employee.NombreCompletoOrdenado()).FontSize(12).FontFamily(Fonts.Arial); // jefe directo
                                

                            });

                        });
                    });
                });
            });



            string nombrePDF = "Prestamo-Acuse-"+prestamo_id;
            var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\Prestamos\PDFsTemporales", $"{nombrePDF}.pdf");
            document.GeneratePdf(rutaCompleta);

            //byte[] archivo = File.ReadAllBytes(rutaCompleta);
            //File.Delete(rutaCompleta);

            return rutaCompleta.Substring(3);
        }
    
        
        public async Task<string> PagarePDF(int prestamo_id)
        {
            Prestamo prestamo = await _repositoryAsyncPrestamo.GetByIdAsync(prestamo_id);
            Employee employee = await _repositoryAsyncEmployee.GetByIdAsync(prestamo.EmployeeId);
            Company company = await _repositoryAsyncCompany.GetByIdAsync(prestamo.CompanyId);

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

                        layers.Layer().TranslateX(100).TranslateY(95).Text("PRÉSTAMOS").FontSize(10).Bold().FontFamily(Fonts.TimesNewRoman);

                        byte[] imageLogo = File.ReadAllBytes("C:\\StaticFiles\\Mate\\img\\wise_LOGO.png");
                        layers.Layer().Width(180).Border(0).TranslateX(30).TranslateY(50).Image(imageLogo);

                        layers.Layer().Height(60).Width(250).TranslateX(310).TranslateY(80).Border(0).AlignRight().Text(text =>
                        {
                            text.Line("CDMX a " + dateTime.ToString("dd 'de' MMMM 'del' yyyy.")).FontSize(10).Bold();
                            text.Line("Oficina: Ciudad de México.").FontSize(10).Bold();
                            text.Line("Folio:   " + 9998).FontSize(10).Bold();
                        });


                    });

                    //.Background(Colors.Grey.Lighten3)
                    page.Content().Border(0).Column(column =>
                    {
                        string fuente_content = Fonts.Arial;
                        //column.Spacing(0);
                        column.Item().AlignCenter().Width(560).Border(0).Column(column =>
                        {
                            column.Item().AlignCenter().Text("FORMATO DE PRÉSTAMO").FontSize(18).Bold().FontFamily(Fonts.Arial).LineHeight(2);
                        });

                        // Datos personales
                        column.Item().Height(20).Layers(layer =>
                        {
                            layer.PrimaryLayer().Border(0);
                            layer.Layer().AlignCenter().Width(560).Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png");
                            layer.Layer().PaddingLeft(25).PaddingTop(3).Text("DATOS PERSONALES").FontSize(10).Bold().FontColor("#FFF").FontFamily(Fonts.TimesNewRoman);
                        });

                        column.Item().AlignCenter().Width(560).Border(0).Column(column =>
                        {
                            column.Item().Border(0).PaddingBottom(-10).Row(row =>
                            {
                                row.RelativeItem(1).Border(0).Background("#FFF").PaddingHorizontal(10).Text(text => {
                                    text.Line("Nombre: "+ employee.NombreCompletoOrdenado()).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Banco: "+ employee.Banco).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Telefono Fijo: "+ employee.TelefonoFijo).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Telefono Celular: " +employee.TelefonoMovil).FontSize(10).FontFamily(fuente_content);
                                });

                                row.RelativeItem(1).Border(0).Background("#FFF").PaddingHorizontal(10).Text(text => {
                                    text.Line("RFC: "+ employee.Rfc).FontSize(10).FontFamily(fuente_content);
                                    text.Line("CLABE: "+ employee.CLABE).FontSize(10).FontFamily(fuente_content);
                                    text.Line("CURP: "+ employee.Curp).FontSize(10).FontFamily(fuente_content);
                                    text.Line("E-Mail: "+ employee.Mail).FontSize(10).FontFamily(fuente_content);
                                });

                            });

                        });

                        // Domicilio

                        column.Item().Height(20).Layers(layer =>
                        {
                            layer.PrimaryLayer().Border(0);
                            layer.Layer().AlignCenter().Width(560).Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png");
                            layer.Layer().PaddingLeft(25).PaddingTop(3).Text("DOMICILIO PARTICULAR").FontSize(10).Bold().FontColor("#FFF").FontFamily(Fonts.TimesNewRoman);
                        });

                        column.Item().AlignCenter().Width(560).Border(0).Column(column =>
                        {
                            column.Item().Border(0).PaddingBottom(-10).Row(row =>
                            {
                                row.RelativeItem().Border(0).Background("#FFF").PaddingHorizontal(10).Text(text => {
                                    text.Line("Calle: "+ employee.Calle).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Colonia: "+ employee.Colonia).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Municipio: "+ employee.Municipio).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Estado: " + employee.Estado).FontSize(10).FontFamily(fuente_content);
                                });

                                row.RelativeItem().Border(0).Background("#FFF").PaddingHorizontal(10).Text(text => {
                                    text.Line("No. Ext: " + employee.NoExt).FontSize(10).FontFamily(fuente_content);
                                    text.Line("No. Int: " + employee.NoInt).FontSize(10).FontFamily(fuente_content);
                                    text.Line("C.P.: "+ employee.CodigoPostal).FontSize(10).FontFamily(fuente_content);
                                });
                            });

                        });

                        // Datos del prestamo

                        column.Item().Height(20).Layers(layer =>
                        {
                            layer.PrimaryLayer().Border(0);
                            layer.Layer().AlignCenter().Width(560).Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png");
                            layer.Layer().PaddingLeft(25).PaddingTop(3).Text("DATOS DEL PRÉSTAMO DEL SOLICITANTE").FontSize(10).Bold().FontColor("#FFF").FontFamily(Fonts.TimesNewRoman);
                        });

                        column.Item().AlignCenter().Width(560).Border(0).Column(column =>
                        {
                            column.Item().Border(0).PaddingBottom(-10).Row(row =>
                            {
                                row.RelativeItem(1).Border(0).Background("#FFF").PaddingHorizontal(10).Text(text => {
                                    text.Line("Importe: $ "+ prestamo.Monto).FontSize(10).FontFamily(fuente_content);
                                });

                                row.RelativeItem(1).Border(0).Background("#FFF").PaddingHorizontal(10).Text(text => {
                                    text.Line("Plazo (Quincena): "+ prestamo.Plazo).FontSize(10).FontFamily(fuente_content);
                                });

                            });

                        });

                        // Datos del prestamo de la empresa
                        column.Item().Height(20).Layers(layer =>
                        {
                            layer.PrimaryLayer().Border(0);
                            layer.Layer().AlignCenter().Width(560).Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png");
                            layer.Layer().PaddingLeft(25).PaddingTop(3).Text("DATOS DEL PRESTAMO OTORGADO POR LA EMPRESA").FontSize(10).Bold().FontColor("#FFF").FontFamily(Fonts.TimesNewRoman);
                        });

                        column.Item().AlignCenter().Width(560).Border(0).Column(column =>
                        {
                            column.Item().Border(0).Row(row =>
                            {
                                string fecha_autorizacion = prestamo.FechaEstatusActivo != null ? prestamo.FechaEstatusActivo.Value.ToString("dd/MM/yyyy") : "Fecha no disponible";
                                string fecha_transferencia = prestamo.FechaTransferencia != null ? prestamo.FechaTransferencia.Value.ToString("dd/MM/yyyy") : "Fecha no disponible";
                                row.RelativeItem().Border(0).Background("#FFF").PaddingHorizontal(10).Text(text => {
                                    text.Line("Taza de interes anual: $ "+ prestamo.Interes).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Total a pagar: $ " + prestamo.Total).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Descuento quincenal: $ "+ prestamo.Descuento).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Fecha de autorizacion: " + fecha_autorizacion).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Fecha de transferencia: " + fecha_transferencia).FontSize(10).FontFamily(fuente_content);
                                });

                                row.RelativeItem().Border(0).Background("#FFF").PaddingHorizontal(10).Text(text => {
                                    text.Line("Taza de Fondo de garantia: "+ prestamo.TazaFondoGarantia).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Interes: "+ prestamo.TazaInteres).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Quincena de inicio: "+ prestamo.PeriodoInicial).FontSize(10).FontFamily(fuente_content);
                                    text.Line("Quincena final: "+ prestamo.PeriodoFinal).FontSize(10).FontFamily(fuente_content);
                                });

                            });

                        });

                        // Protesta
                        column.Item().AlignCenter().Width(560).Border(0).Column(column =>
                        {
                            column.Item().Text("Por este pagaré me obligo incondicionalmente a pagar el total del préstamo solicitado, " +
                                "incluyendo las condiciones finan" +
                                "cieras establecidas; valor recibido a mi entera satisfacción en calidad de préstamo, " +
                                "comprometiéndome a liquidarlo en los plazos pactados, que cubriré sin excepción ni demora, si por cualquier razón no " +
                                "se hiciera la retención debida como abono a este préstamo, me comprometo a depositar inmediatamente a la cuenta designada " +
                                "el importe correspondiente. En caso de retraso en los pagos por causa imputable a mi persona (Insuficiencia de fondos, cuentas canceladas, " +
                                "cuentas bloqueadas, cuenta inexistente, baja de domiciliación, bloqueo de transferencia y permiso sin goce de sueldo u otros), me obligo a " +
                                "liquidar el saldo pendiente del préstamo de acuerdo con lo dispuesto en el Reglamento vigente. Así mismo, manifiesto que, reconozco " +
                                "y acepto expresamente las consecuencias de no pagar cualquier abono en el plazo estipulado, que corresponde a pagar un interés " +
                                "adicional a lo ya pactado, por cada una de las omisiones de pago hasta el día de su liquidación en los mismos términos del interés ordinario.")
                                .FontSize(8).FontFamily(Fonts.Arial);

                            column.Item().Text("En pleno uso de mis facultades, en caso de proceder el dictamen de mi jubilación o invalidez, con base en la " +
                                "legislación vigente del IMSS autorizo a la empresa que descuente de forma automática, el saldo pendiente de mi préstamo activo " +
                                "a través de mi Cuenta Individual WISE, teniendo derecho a recibir la diferencia que resulte a mi favor como remanente de mi " +
                                "cuenta individual WISE. De igual forma, en los casos de separación laboral obligatoria o voluntaria, autorizo a la empresa a que " +
                                "me descuente el saldo pendiente de mi Cuenta Individual WISE del pago del préstamo que solicité, y en caso de mantener una diferencia " +
                                "positiva, recibir únicamente esta diferencia.")
                                .FontSize(8).FontFamily(Fonts.Arial);


                            column.Item().AlignCenter().Row(row => {
                                row.Spacing(50);

                                row.AutoItem().AlignCenter().PaddingTop(60).Column(column => {

                                    //column.Item().AlignCenter().Text("Atentamente").FontSize(12).FontFamily(Fonts.Arial);
                                    column.Item().Width(200).PaddingTop(20).LineHorizontal(1).LineColor(Colors.Black);
                                    //column.Item().AlignCenter().Text("Firma y fecha").FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                    column.Item().AlignCenter().Text(employee.NombreCompletoOrdenado()).FontSize(12).FontFamily(Fonts.Arial); // jefe directo
                                                                                                                                   //column.Item().AlignCenter().Text("Solicitante").FontSize(12).FontFamily(Fonts.TimesNewRoman);

                                });

                            });

                            column.Item().Text("Requisitos: Identificación oficial, los tres últimos recibos de nómina, estado de cuenta bancario de los últimos dos meses, " +
                                "comprobante de domicilio no mayor a tres meses.")
                                .FontSize(8).FontFamily(Fonts.Arial);
                        });
                    });
                    //.Background(Colors.Grey.Lighten1)
                    page.Footer().Height(50).Border(0).AlignCenter().Padding(10).Text(text =>
                    {
                        text.Line("Cerro del Borrego 107 , Int. 6, Campestre Churubusco C.P. 04200, Coyoacán, CDMX").FontSize(10).FontFamily(Fonts.Verdana).FontColor("#808080").Bold();
                        //text.DefaultTextStyle(x => x.Size(16));

                        //text.CurrentPageNumber();
                        //text.Span(" / ");
                        //text.TotalPages();
                    });
                });
            });


            string nombrePDF = "Pagare-"+ prestamo_id;
            var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\Prestamos\PDFsTemporales", $"{nombrePDF}.pdf");
            document.GeneratePdf(rutaCompleta);

            //byte[] archivo = File.ReadAllBytes(rutaCompleta);
            //File.Delete(rutaCompleta);

            return rutaCompleta.Substring(3);
        }


        public async Task<string> EstadoCuentaPDF(int prestamo_id)
        {
            Prestamo prestamo = await _repositoryAsyncPrestamo.GetByIdAsync(prestamo_id);
            Employee employee = await _repositoryAsyncEmployee.GetByIdAsync(prestamo.EmployeeId);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(1);

                    //page.Header().Height(100).Background(Colors.Grey.Lighten1);

                    page.Header().Height(120).Border(0).Layers(layers =>
                    {

                        layers.PrimaryLayer().Border(0);


                        byte[] imageLogo1 = File.ReadAllBytes("C:\\StaticFiles\\Mate\\img\\soluciones.jpg");
                        layers.Layer().Width(180).Border(0).TranslateX(30).TranslateY(50).Image(imageLogo1);

                        byte[] imageLogo2 = File.ReadAllBytes("C:\\StaticFiles\\Mate\\img\\wise_prestamos.png");
                        layers.Layer().Width(180).Border(0).TranslateX(310).TranslateY(60).Image(imageLogo2);

                        layers.Layer().TranslateX(350).TranslateY(40).Text("ESTADO DE CUENTA").FontSize(15).Bold().FontFamily(Fonts.Calibri);
                    });

                    //.Background(Colors.Grey.Lighten3)
                    page.Content().Border(0).Column(column =>
                    {
                        string fuente_content = Fonts.Arial;

                        // Contenido
                        column.Item().PaddingTop(10).AlignCenter().Width(560).Border(0).Column(column =>
                        {
                            column.Spacing(10);

                            column.Item().MaxHeight(280).Border(0).Row(row =>
                            {
                                row.Spacing(20);

                                //COLUMNA 1
                                row.RelativeItem().Border(0).Column(column =>
                                {

                                    column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(15).Layers(layer =>
                                    {
                                        layer.PrimaryLayer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                        layer.Layer().PaddingLeft(15).PaddingTop(0).Text("INFORMACION PERSONAL").FontSize(10).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                    });

                                    column.Item().Border(1, Unit.Point).BorderColor("#88bddb").PaddingHorizontal(10).PaddingTop(5).Text(text =>
                                    {

                                        text.Line("NOMBRE: " + employee.NombreCompletoOrdenado()).FontSize(9).FontFamily(fuente_content).Bold();
                                        text.Line("RFC: " + employee.Rfc).FontSize(9).FontFamily(fuente_content).Bold();
                                        text.Line("CURP: " + employee.Curp).FontSize(9).FontFamily(fuente_content).Bold();
                                        text.Line("ID DEL EMPLEADO: " + employee.Id).FontSize(9).FontFamily(fuente_content).Bold();
                                        text.Line("BANCO: " + employee.Banco).FontSize(9).FontFamily(fuente_content).Bold();
                                        text.Line("PUESTO O CARGO: " + employee.Puesto).FontSize(9).FontFamily(fuente_content).Bold();
                                        text.Line("No. DE CUENTA: " + employee.NoCuenta).FontSize(9).FontFamily(fuente_content).Bold();
                                        text.Line("E-Mail: " + employee.Mail).FontSize(9).FontFamily(fuente_content).Bold();
                                    });


                                });


                                //COLUMNA 2
                                row.RelativeItem().Border(0).Column(column =>
                                {
                                    column.Spacing(20);

                                    column.Item().Column(column =>
                                    {
                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(15).Layers(layer =>
                                        {
                                            layer.PrimaryLayer().Border(0);
                                            layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                            layer.Layer().PaddingLeft(15).PaddingTop(0).Text("ESTADO DE CUENTA").FontSize(10).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                        });


                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").PaddingHorizontal(10).PaddingTop(5).Text(text =>
                                        {
                                            text.Line("MONTO TOTAL A PAGAR: $ " + prestamo.Total).FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("No. DE PLAZO: " + prestamo.Plazo).FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("CAPITAL: $ " + prestamo.Monto).FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("INTERES: $ " + prestamo.Interes).FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("FONDO DE GARANTIA: $ " + prestamo.FondoGarantia).FontSize(9).FontFamily(fuente_content).Bold();

                                        });

                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(15).Layers(layer =>
                                        {
                                            layer.PrimaryLayer().Border(0);
                                            layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                            layer.Layer().PaddingLeft(15).PaddingTop(0).Text("DESCUENTO QUINCENAL :").FontSize(10).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                        });
                                    });

                                    column.Item().Column(column =>
                                    {
                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(15).Layers(layer =>
                                        {
                                            layer.PrimaryLayer().Border(0);
                                            layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                            layer.Layer().PaddingLeft(15).PaddingTop(0).Text("RESUMEN DEL PERIODO").FontSize(10).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                        });


                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").PaddingHorizontal(10).PaddingTop(5).Text(text =>
                                        {


                                            text.Line("SALDO PENDIENTE: $ 5,000.00").FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("QUINCENA: 202102").FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("CAPITAL: $ 500.00").FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("INTERES: $ 0.00").FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("FONDO DE GARANTIA: $ 5,500.00").FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("RECUPERACION: EXITOSOS: 11 NO EXITOSOS: 0").FontSize(9).FontFamily(fuente_content).Bold();

                                        });


                                    });

                                });
                            });

                            column.Item().Border(0).Column(column =>
                            {
                                column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(20).Layers(layer =>
                                {
                                    layer.PrimaryLayer().Border(0);
                                    layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                    layer.Layer().PaddingLeft(15).PaddingTop(0).Text("DESGLOSE ESTADO DE CUENTA").FontSize(10).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                });

                                column.Item().AlignCenter().Column(column => {
                                    column.Item().Border(0)
                                    .Table(table =>
                                    {

                                        // Datos de ejemplo para tuColeccionDeDatos
                                        var tuColeccionDeDatos = new List<(int id,
                                                                        int quincena,
                                                                        string concepto,
                                                                        double descuento,
                                                                        double capital,
                                                                        double interes,
                                                                        double fondeo_garantia,
                                                                        double moratorio,
                                                                        double moratorio_pagado,
                                                                        double saldo)>
                                                {
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),
                                    (1,202401,"EXITOSO", 1551.25, 36258.14, 11.01, 0.00, 3.14, 2.71, 1.61),

                                                };

                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(1);
                                            columns.RelativeColumn(2);
                                            columns.RelativeColumn(2);
                                            columns.RelativeColumn(2);
                                            columns.RelativeColumn(2);
                                            columns.RelativeColumn(2);
                                            columns.RelativeColumn(2);
                                            columns.RelativeColumn(2);
                                            columns.RelativeColumn(2);
                                            columns.RelativeColumn(2);
                                        });

                                        int size_encabezado = 8;

                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("#").FontSize(size_encabezado); text.AlignCenter(); });
                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("QUINCENA").FontSize(size_encabezado); text.AlignCenter(); });
                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("CONCEPTO").FontSize(size_encabezado); text.AlignCenter(); });
                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("DESCUENTO").FontSize(size_encabezado); text.AlignCenter(); });
                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("CAPITAL").FontSize(size_encabezado); text.AlignCenter(); });
                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("INTERES").FontSize(size_encabezado); text.AlignCenter(); });
                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("FONDO DE GARANTIA").FontSize(size_encabezado); text.AlignCenter(); });
                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("MORATORIO").FontSize(size_encabezado); text.AlignCenter(); });
                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("MORATORIO PAGADO").FontSize(size_encabezado); text.AlignCenter(); });
                                        table.Cell().ColumnSpan(1).Border(0).Height(25).Background("#d1d1d1").Text(text => { text.Span("SALDO").FontSize(size_encabezado); text.AlignCenter(); });


                                        int size_filas = 8;
                                        // Contenido de la tabla
                                        foreach (var fila in tuColeccionDeDatos)
                                        {
                                            table.Cell().Border(0).AlignCenter().Text(fila.id).FontSize(size_filas);
                                            table.Cell().Border(0).AlignCenter().Text(fila.quincena).FontSize(size_filas);
                                            table.Cell().Border(0).AlignCenter().Text(fila.concepto).FontSize(size_filas);
                                            table.Cell().Border(0).AlignCenter().Text("$ " + fila.descuento).FontSize(size_filas);
                                            table.Cell().Border(0).AlignCenter().Text("$ " + fila.capital).FontSize(size_filas);
                                            table.Cell().Border(0).AlignCenter().Text("$ " + fila.interes).FontSize(size_filas);
                                            table.Cell().Border(0).AlignCenter().Text("$ " + fila.fondeo_garantia).FontSize(size_filas);
                                            table.Cell().Border(0).AlignCenter().Text("$ " + fila.moratorio).FontSize(size_filas);
                                            table.Cell().Border(0).AlignCenter().Text("$ " + fila.moratorio_pagado).FontSize(size_filas);
                                            table.Cell().Border(0).AlignCenter().Text("$ " + fila.saldo).FontSize(size_filas);
                                        }



                                    });
                                });
                            });

                            column.Item().Border(0).ExtendVertical().Row(row =>
                            {


                                row.RelativeItem().AlignBottom().Row(row =>
                                {
                                    row.Spacing(10);
                                    row.RelativeItem(2).Border(0).Height(110).Column(column =>
                                    {
                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(15).Layers(layer =>
                                        {
                                            layer.PrimaryLayer().Border(0);
                                            layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                            layer.Layer().PaddingLeft(15).PaddingTop(0).Text("SALDO PENDIENTE").FontSize(10).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                        });


                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").PaddingHorizontal(10).PaddingTop(5).Text(text =>
                                        {
                                            text.Line("CAPITAL PENDIENTE POR RECUPERAR: ( + ) $   $ 5,000.00").FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("FONDE DE GRARANTIA PENDIENTE POR RECUPERAR: ( + ) $    $10").FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("INTERESES DEVENGADO: ( + ) $    $ 0.00").FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("INTERESES MORATORIOS: ( + ) $    $ 0.00").FontSize(9).FontFamily(fuente_content).Bold();
                                            text.Line("INTERESES CONDONADOS: ( + ) $    $ 0.00").FontSize(9).FontFamily(fuente_content).Bold();

                                        });

                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(15).Layers(layer =>
                                        {
                                            layer.PrimaryLayer().Border(0);
                                            layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                            layer.Layer().PaddingLeft(15).PaddingTop(0).Text("SALDO PARA LIQUIDAR").FontSize(10).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                        });
                                    });

                                    row.RelativeItem(1).Column(column =>
                                    {
                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(30).Layers(layer =>
                                        {
                                            layer.PrimaryLayer().Border(0);
                                            layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                            layer.Layer().PaddingLeft(15).AlignCenter().PaddingTop(0).Text("DATOS PARA REALIZAR EL DEPOSITO DE LIQUIDACION").FontSize(10).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                        });


                                        column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(78).PaddingHorizontal(10).PaddingTop(5).Text(text =>
                                        {


                                            text.Line("INSTITUCIÓN BANCARIA: SANTANDER MÉXICO S.A.").FontSize(6).FontFamily(fuente_content).Bold();
                                            text.Line("CUENTA EJE: 65505217428").FontSize(6).FontFamily(fuente_content).Bold();
                                            text.Line("CUENTA CLABE: 014180655052174287").FontSize(6).FontFamily(fuente_content).Bold();
                                            text.Line("BENEFICIARIO: CONSULTORÍA INTEGRAL").FontSize(6).FontFamily(fuente_content).Bold();
                                            text.Line("EN DESARROLLO E INNOVACIÓN ACTUARIAL").FontSize(6).FontFamily(fuente_content).Bold();

                                        });


                                    });


                                });
                            });
                        });
                    });

                    page.Footer().Height(50).Border(0).AlignCenter().Padding(10).Column(column =>
                    {
                        //column.Item().Width(560).PaddingTop(5).LineHorizontal(1).LineColor(Colors.Black);
                        column.Item().Width(560).BorderTop(1, Unit.Point).BorderColor(Colors.Black);
                        column.Item().AlignCenter().Text("INGRESA A WWW.CIPE.MX Y CONOCE TUS BENEFICIOS").FontSize(9).FontFamily(Fonts.Calibri);

                    });
                });
            });


            string nombrePDF = "Estado-Cuenta-" + prestamo_id;
            var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\Prestamos\PDFsTemporales", $"{nombrePDF}.pdf");
            document.GeneratePdf(rutaCompleta);

            //byte[] archivo = File.ReadAllBytes(rutaCompleta);
            //File.Delete(rutaCompleta);

            return rutaCompleta.Substring(3);
        }


    }
}
