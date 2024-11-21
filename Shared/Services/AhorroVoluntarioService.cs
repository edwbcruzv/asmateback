using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Domain.Entities;
using Application.Specifications;
using Microsoft.AspNetCore.Http;
using Application.Exceptions;
using Domain.Enums;

namespace Shared.Services
{
    public class AhorroVoluntarioService : IAhorroVoluntarioService
    {
        private readonly IFilesManagerService _filesManagerService;
        private readonly IRepositoryAsync<Nomina> _repositoryNomina;
        private readonly IRepositoryAsync<NominaDeduccion> _repositoryAsyncNominaDeducciones;
        private readonly IRepositoryAsync<Employee> _repositoryEmployee;
        private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAhorroVoluntario;
        private readonly ISendMailService _sendMailService;

        public AhorroVoluntarioService(IFilesManagerService filesManagerService, 
                    IRepositoryAsync<Nomina> repositoryNomina, 
                    IRepositoryAsync<NominaDeduccion> repositoryAsyncNominaDeducciones, 
                    IRepositoryAsync<Employee> repositoryEmployee, 
                    IRepositoryAsync<AhorroVoluntario> repositoryAhorroVoluntario, 
                    ISendMailService sendMailService)
        {
            _filesManagerService = filesManagerService;
            _repositoryNomina = repositoryNomina;
            _repositoryAsyncNominaDeducciones = repositoryAsyncNominaDeducciones;
            _repositoryEmployee = repositoryEmployee;
            _repositoryAhorroVoluntario = repositoryAhorroVoluntario;
            _sendMailService = sendMailService;
        }

        public void SetFechaAndEstatus(AhorroVoluntario ahorro_voluntario, EstatusOperacion estatus)
        {
            ahorro_voluntario.Estatus = estatus;
            switch (estatus)
            {
                case EstatusOperacion.Pendiente:
                    ahorro_voluntario.FechaEstatusPendiente = DateTime.Now;
                    break;

                case EstatusOperacion.Activo:
                    ahorro_voluntario.FechaEstatusActivo = DateTime.Now;
                    break;

                case EstatusOperacion.Finiquitado:
                    ahorro_voluntario.FechaEstatusFiniquitado = DateTime.Now;
                    break;

                case EstatusOperacion.Rechazado:
                    ahorro_voluntario.FechaEstatusRechazado = DateTime.Now;
                    break;

                default:
                    break;
            }
        }

        public string SaveCartaFirmada(IFormFile file_carta_firmada, int ahorro_id)
        {
            return _filesManagerService.saveFileInTo(file_carta_firmada,ahorro_id , "StaticFiles\\Mate", "FormatosPDF\\AhorrosVoluntario\\CartasFirmadas", ".pdf");
        }

        public async Task<bool> EnviarCorreoEstatus(AhorroVoluntario ahorro)
        {
            Employee employee = await _repositoryEmployee.GetByIdAsync(ahorro.EmployeeId);

            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarSolicitudPlanes.html")).ToString();

            string estatus = ahorro.Estatus.ToString();

            if (ahorro.Estatus == EstatusOperacion.Activo)
            {
                estatus = "Autorizado";
            }

            mailHTML = mailHTML.Replace("#_TITULO_#", "Ahorro Voluntario");
            mailHTML = mailHTML.Replace("#_SUBTITULO_#", "Estatus de operación");

            if(ahorro.Estatus == EstatusOperacion.Pendiente)
            {
                mailHTML = mailHTML.Replace("#_SECCION1_#", $"El empleado(a) {employee.NombreCompletoOrdenado()}, a solicitado un ahorro voluntario.");
            }
            else
            {
                mailHTML = mailHTML.Replace("#_SECCION1_#", $"Estimado(a) {employee.NombreCompletoOrdenado()}, su ahorro a sido  {estatus}.");
            }

            if (employee == null)
            {
                throw new ApiException($"Empleado con Id {ahorro.EmployeeId} no existe.");
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
                _sendMailService.SendEmailWithAttachment("facturacion@maxal.com.mx", lista_correos, "Estatus de ahorro voluntario", mailHTML, null);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return true;


        }

        public async Task<double> GetDeduccion(int employee_id)
        {
            try
            {

                // se obtiene la ultima nomina
                var list_nominas = await _repositoryNomina.ListAsync(new NominasByEmployeeIdSpecification(employee_id));
                Nomina ultima_nomina = list_nominas[list_nominas.Count - 1];

                // se obtiene la ultima deduccion en base a la ultima nomina
                var list_nomina_deducciones = await _repositoryAsyncNominaDeducciones.ListAsync(new NominaDeduccionesByNominaAndClaveSpecification(ultima_nomina.Id,"048"));
                NominaDeduccion ultima_nomina_deduccion = list_nomina_deducciones[list_nomina_deducciones.Count-1];


                return ultima_nomina_deduccion.Importe;
            }catch(Exception ex)
            {
                return 0.0;
            }


        }

        public async Task<string> EstadoCuentaPDF(int ahorro_id, int periodo_inicial, int periodo_final)
        {
            AhorroVoluntario ahorro_voluntario = await _repositoryAhorroVoluntario.GetByIdAsync(ahorro_id);
            Employee employee = await _repositoryEmployee.GetByIdAsync(ahorro_voluntario.EmployeeId);
            var nombre = employee.NombreCompletoOrdenado();

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

                        byte[] imageLogo2 = File.ReadAllBytes("C:\\StaticFiles\\Mate\\img\\wise_edo_retiro.png");
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

                            column.Item().MaxHeight(230).Border(0).Row(row =>
                            {
                                row.Spacing(20);

                                row.RelativeItem().Border(0).Column(column =>
                                {
                                    //COLUMNA 1
                                    column.Item().Border(0).Column(column =>
                                    {
                                        column.Spacing(10);

                                        column.Item().Column(column =>
                                        {
                                            column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(18).Layers(layer =>
                                            {
                                                layer.PrimaryLayer().Border(0);
                                                layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                                layer.Layer().PaddingLeft(15).PaddingTop(0).Text("INFORMACION PERSONAL").FontSize(12).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                            });
                                            column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(130).PaddingHorizontal(10).PaddingTop(5).Row(row =>
                                            {

                                                row.RelativeItem(1).Border(0).Text(text =>
                                                {

                                                    text.Line("NOMBRE: "+ employee.NombreCompletoOrdenado()).FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("RFC: "+ employee.Rfc).FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("CURP: "+ employee.Curp).FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("ID DEL EMPLEADO: "+ employee.Id).FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("BANCO: "+ employee.Banco).FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("PUESTO O CARGO: "+ employee.Puesto).FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("No. DE CUENTA: "+ employee.NoCuenta).FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("E-Mail: "+ employee.Mail).FontSize(10).FontFamily(fuente_content).Bold();


                                                });
                                            });
                                        });

                                        column.Item().Column(column =>
                                        {
                                            column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(20).Layers(layer =>
                                            {
                                                layer.PrimaryLayer().Border(0);
                                                layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                                layer.Layer().PaddingLeft(15).PaddingTop(0).Text("SUELDO TOTAL").FontSize(12).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                            });

                                            column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(15).Text(" $ 5,500.00").FontSize(10).FontFamily(fuente_content).Bold();

                                        });

                                    });

                                });


                                row.RelativeItem().Border(0).Column(column =>
                                {
                                    //COLUMNA 2

                                    column.Item().Column(column =>
                                    {
                                        column.Spacing(10);

                                        column.Item().Column(column =>
                                        {
                                            column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(20).Layers(layer =>
                                            {
                                                layer.PrimaryLayer().Border(0);
                                                layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                                layer.Layer().PaddingLeft(15).PaddingTop(0).Text("PERIODO").FontSize(12).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                            });

                                            column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(15).Text(" 202017 - 202102").FontSize(10).FontFamily(fuente_content).Bold();
                                        });


                                        column.Item().Column(column =>
                                        {
                                            column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(20).Layers(layer =>
                                            {
                                                layer.PrimaryLayer().Border(0);
                                                layer.Layer().Image("C:\\StaticFiles\\Mate\\img\\BARRA_CORTA_2.png").FitUnproportionally();
                                                layer.Layer().PaddingLeft(15).PaddingTop(0).Text("RESUMEN DEL PERIODO").FontSize(12).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                            });

                                            column.Item().Border(1, Unit.Point).BorderColor("#88bddb").Height(130).Row(row =>
                                            {

                                                row.RelativeItem(1).Border(0).PaddingHorizontal(10).Text(text =>
                                                {

                                                    text.Line("SALDO ANTERIOR: $ 5,000.00").FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("QUINCENA: 202102").FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("APORTACION: $ 500.00").FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("RENDIMIENTO: $ 0.00").FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("SALDO ACTUAL: $ 5,500.00").FontSize(10).FontFamily(fuente_content).Bold();
                                                    text.Line("RECUPERACION: EXITOSOS: 11 NO EXITOSOS: 0").FontSize(10).FontFamily(fuente_content).Bold();
                                                });
                                            });
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
                                    layer.Layer().PaddingLeft(15).PaddingTop(0).Text("RESUMEN GENERAL").FontSize(12).FontColor("#FFF").FontFamily(Fonts.Calibri);

                                });
                                var tuColeccionDeDatos = new List<(int periodo, double total, double saldo)>
                                            {
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.10),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.01 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.20),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                    (202017, 500.00 , 1000.00),
                                            };

                                column.Item().AlignCenter().PaddingVertical(5).Row(row =>
                                {
                                    row.RelativeItem().AlignCenter().Border(0).Height(15).Text("PERIODO").FontSize(10).Bold().FontFamily(Fonts.Arial);
                                    row.RelativeItem().AlignCenter().Border(0).Height(15).Text("TOTAL BIMESTRAL").FontSize(10).Bold().FontFamily(Fonts.Arial);
                                    row.RelativeItem().AlignCenter().Border(0).Height(15).Text("SALDO").FontSize(10).Bold().FontFamily(Fonts.Arial);
                                });

                                foreach (var fila in tuColeccionDeDatos)
                                {
                                    column.Item().AlignCenter().PaddingVertical(3).Row(row =>
                                    {
                                        row.RelativeItem().AlignCenter().Border(0).Height(15).Text($"$ {fila.periodo}").FontSize(10).Bold().FontFamily(Fonts.Arial);
                                        row.RelativeItem().AlignCenter().Border(0).Height(15).Text($"$ {fila.total}").FontSize(10).Bold().FontFamily(Fonts.Arial);
                                        row.RelativeItem().AlignCenter().Border(0).Height(15).Text($"$ {fila.saldo}").FontSize(10).Bold().FontFamily(Fonts.Arial);

                                    });

                                }




                            });

                            column.Item().Height(25).Border(1).Row(row =>
                            {


                                row.RelativeItem().Row(row =>
                                {
                                    row.Spacing(10);
                                    row.RelativeItem().AlignCenter().Width(25).Border(1).Image("C:\\StaticFiles\\Mate\\img\\facebook.png");
                                    row.RelativeItem(4).AlignMiddle().Border(1).Height(15).Text("Soluciones Integrales MX").FontSize(10).Bold().FontFamily(Fonts.Arial);
                                    row.RelativeItem().AlignCenter().Width(25).Border(1).Image("C:\\StaticFiles\\Mate\\img\\twitter.png");
                                    row.RelativeItem(4).AlignMiddle().Border(1).Height(15).Text("Soluciones Integrales MX").FontSize(10).Bold().FontFamily(Fonts.Arial);
                                    row.RelativeItem().AlignCenter().Width(25).Border(1).Image("C:\\StaticFiles\\Mate\\img\\youtube.png");
                                    row.RelativeItem(4).AlignMiddle().Border(1).Height(15).Text("@IntegralesMx").FontSize(10).Bold().FontFamily(Fonts.Arial);

                                });


                            });
                        });
                    });

                    page.Footer().Height(50).Border(0).AlignCenter().Padding(10).Text(text =>
                    {
                        text.Line("Cerro del Borrego 107 , Int. 6, Campestre Churubusco C.P. 04200, Coyoacán, CDMX").FontSize(10).FontFamily(Fonts.Verdana).FontColor("#808080").Bold();

                    });
                });
            });

            string nombrePDF = "EstadoCuenta-" + ahorro_id;
            var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\AhorrosVoluntario\PDFsTemporales", $"{nombrePDF}.pdf");
            document.GeneratePdf(rutaCompleta);
            return rutaCompleta.Substring(3);
        }



        public async Task<string> CartaPDF(int ahorro_id)
        {
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
                            text.Line("Oficina: Ciudad de México.").FontSize(10).Bold();
                            text.Line("Folio:   " + 9998).FontSize(10).Bold();
                        });


                    });

                    //.Background(Colors.Grey.Lighten3)
                    page.Content().Border(0).Column(column =>
                    {
                        string fuente_content = Fonts.Arial;
                        column.Spacing(10);
                        column.Item().AlignCenter().Width(540).Border(0).Column(column =>
                        {
                            column.Item().AlignCenter().Text("CARTA DE AHORRO VOLUNTARIO").FontSize(18).Bold().FontFamily(Fonts.Arial).LineHeight(2);
                        });

                        // Contenido
                        column.Item().AlignCenter().Width(510).Border(0).Column(column =>
                        {
                            column.Spacing(20);

                            column.Item().Text("Por este medio, en pleno uso de mis facultades, autorizo expresamente a la empresa Consultoría y " +
                                "Administración de Empresas DAE antes Soluciones Integrales, para aplicar el descuento por un importe de $1000.00 " +
                                "(UN MIL PESOS 00 /100 M.N.)quincenales. De lo anterior acepto ser susceptible de la aplicación de descuentos en la " +
                                "nómina por concepto de “OTROS” con clave 004")
                                .FontSize(12).FontFamily(Fonts.Arial);

                            column.Item().Text("En caso de mi fallecimiento autorizo a la empresa Consultoría y Administración de Empresas DAE antes " +
                                "Soluciones Integrales, en pleno uso de mis facultades mentales que entregue el monto de mi ahorro voluntario junto con " +
                                "sus rendimientos a los beneficiarios que designo a continuación.")
                                .FontSize(12).FontFamily(Fonts.Arial);

                            column.Item().AlignCenter().Column(column => {
                                column.Item().Border(0)
                                .Table(table =>
                                {

                                    // Datos de ejemplo para tuColeccionDeDatos
                                    var tuColeccionDeDatos = new List<(int id, string ApellidoPaterno, string ApellidoMaterno, string Nombre, string Parentesco, int Porcentaje)>
                                            {
                                    (1,"Perez","Avila","Jose Luis","Abuelo", 50),
                                    (1,"Perez","Avila","Jose Luis","Abuelo", 50),
                                    (1,"Perez","Avila","Jose Luis","Abuelo", 50),
                                    (1,"Perez","Avila","Jose Luis","Abuelo", 50),
                                    (1,"Perez","Avila","Jose Luis","Abuelo", 50),
                                    (1,"Perez","Avila","Jose Luis","Abuelo", 50),
                                    (1,"Perez","Avila","Jose Luis","Abuelo", 50),
                                    (1,"Perez","Avila","Jose Luis","Abuelo", 50),
                                            };

                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(3);
                                        columns.RelativeColumn(3);
                                        columns.RelativeColumn(3);
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(2);
                                    });

                                    table.Cell().ColumnSpan(1).Border(1).Height(20).Background("#7d90ed").Text(text => { text.Span("#"); text.AlignCenter(); });
                                    table.Cell().ColumnSpan(1).Border(1).Height(20).Background("#7d90ed").Text(text => { text.Span("Apellido Paterno"); text.AlignCenter(); });
                                    table.Cell().ColumnSpan(1).Border(1).Height(20).Background("#7d90ed").Text(text => { text.Span("Apellido Materno"); text.AlignCenter(); });
                                    table.Cell().ColumnSpan(1).Border(1).Height(20).Background("#7d90ed").Text(text => { text.Span("Nombre(s)"); text.AlignCenter(); });
                                    table.Cell().ColumnSpan(1).Border(1).Height(20).Background("#7d90ed").Text(text => { text.Span("Parentesco"); text.AlignCenter(); });
                                    table.Cell().ColumnSpan(1).Border(1).Height(20).Background("#7d90ed").Text(text => { text.Span("Porcentaje"); text.AlignCenter(); });

                                    // Contenido de la tabla
                                    foreach (var persona in tuColeccionDeDatos)
                                    {
                                        table.Cell().Border(1).AlignCenter().Text(persona.id);
                                        table.Cell().Border(1).AlignCenter().Text(persona.ApellidoPaterno);
                                        table.Cell().Border(1).AlignCenter().Text(persona.ApellidoMaterno);
                                        table.Cell().Border(1).AlignCenter().Text(persona.Nombre);
                                        table.Cell().Border(1).AlignCenter().Text(persona.Parentesco);
                                        table.Cell().Border(1).AlignCenter().Text("% " + persona.Porcentaje);
                                    }



                                });
                            });



                            column.Item().Text("En caso de nombrar a un menor de edad, se deberá asignar a un tutor o albacea. En caso de no asignar " +
                                "beneficiarios, se actuará conforme al artículo 501 de la Ley Federal del Trabajo.")
                                .FontSize(12).FontFamily(Fonts.Arial);



                            column.Item().AlignCenter().Row(row => {
                                row.Spacing(50);

                                row.AutoItem().AlignCenter().PaddingTop(60).Column(column => {

                                    //column.Item().AlignCenter().Text("Atentamente").FontSize(12).FontFamily(Fonts.Arial);
                                    column.Item().Width(200).PaddingTop(20).LineHorizontal(1).LineColor(Colors.Black);
                                    //column.Item().AlignCenter().Text("Firma y fecha").FontSize(12).FontFamily(Fonts.TimesNewRoman);
                                    column.Item().AlignCenter().Text("Nombre ----").FontSize(12).FontFamily(Fonts.Arial); // jefe directo
                                                                                                                                   //column.Item().AlignCenter().Text("Solicitante").FontSize(12).FontFamily(Fonts.TimesNewRoman);

                                });

                            });

                            column.Item().Text("NOTA:\nIntegrar a la Carta de Adhesión los siguientes documentos:\n" +
                                "Copia del recibo de nómina; Copia de Identificación Oficial; Comprobante de domicilio vigente y; " +
                                "Carta de asignación de beneficiarios.")
                                .FontSize(10).FontFamily(Fonts.Arial);
                        });
                    });

                    page.Footer().Height(50).Border(0).AlignCenter().Padding(10).Text(text =>
                    {
                        text.Line("Cerro del Borrego 107 , Int. 6, Campestre Churubusco C.P. 04200, Coyoacán, CDMX").FontSize(10).FontFamily(Fonts.Verdana).FontColor("#808080").Bold();

                    });
                });
            });

            string nombrePDF = "Carta-" + ahorro_id;
            var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\AhorrosVoluntario\PDFsTemporales", $"{nombrePDF}.pdf");
            document.GeneratePdf(rutaCompleta);

            //byte[] archivo = File.ReadAllBytes(rutaCompleta);
            //File.Delete(rutaCompleta);

            return rutaCompleta.Substring(3);
        }





        public async Task<string> SolicitudRetiroPDF(int ahorro_id, float cantidad)
        {
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
                                $"de Beneficios WISE, yo {nombre}, por medio del presente, solicito la cantidad de $ {Math.Round(cantidad,2)} " +
                                $"(PESOS 00 /100 M.N.), procedentes de mi subcuenta de Ahorro Voluntario.")
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

            string nombrePDF = "SolicitudRetiro-" + ahorro_id;
            var rutaCompleta = Path.Combine(@"C:\StaticFiles\Mate\FormatosPDF\AhorrosVoluntario\PDFsTemporales", $"{nombrePDF}.pdf");
            document.GeneratePdf(rutaCompleta);
            return rutaCompleta.Substring(3);

        }

    }
}
