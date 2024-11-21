using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Shared.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
        private readonly IRepositoryAsync<ComplementoPago> _repositoryAsyncComplementoPago;
        private readonly IRepositoryAsync<Client> _repositoryAsyncClient;
        private readonly IRepositoryAsync<Nomina> _repositoryAsyncNomina;
        private readonly IPdfService _pdfService;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

        public SendMailService(
            IRepositoryAsync<Factura> repositoryAsyncFactura,
            IRepositoryAsync<Client> repositoryAsyncClient,
            IPdfService pdfService,
            IRepositoryAsync<ComplementoPago> repositoryAsyncComplementoPago,
            IRepositoryAsync<Nomina> repositoryAsyncNomina,
            IRepositoryAsync<Employee> repositoryAsyncEmployee)
        {
            _repositoryAsyncFactura = repositoryAsyncFactura;
            _repositoryAsyncClient = repositoryAsyncClient;
            _pdfService = pdfService;
            _repositoryAsyncComplementoPago = repositoryAsyncComplementoPago;
            _repositoryAsyncNomina = repositoryAsyncNomina;
            _repositoryAsyncNomina = repositoryAsyncNomina;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
        }

        public async Task<Response<bool>> sendComplementoPago(int Id)
        {
            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarComplementoPago.html")).ToString();

            var complementoPago = await _repositoryAsyncComplementoPago.GetByIdAsync(Id);

            var client = await _repositoryAsyncClient.GetByIdAsync(complementoPago.ClientId);

            if (client == null)
            {
                throw new ApiException($"Cliente con Id ${complementoPago.ClientId} en existe en clientes");
            }

            if (client.Correos == null || client.Correos.Equals(""))
            {
                throw new ApiException($"Cliente no cuenta con correo registrado");
            }

            var correos = client.Correos.Split(";");

            var attachmentPaths = new List<string>();

            var pdf = await _pdfService.PdfComplementoPago(complementoPago.Id);
            var pdfPath = Path.Combine(@"C:\", pdf.Data.SourceFile);

            attachmentPaths.Add(pdfPath);

            if (complementoPago.FileXmlTimbrado != null && !complementoPago.FileXmlTimbrado.Equals(""))
            {
                attachmentPaths.Add(Path.Combine(@"C:\", complementoPago.FileXmlTimbrado));
            }

            try
            {
                SendEmailWithAttachment("facturacion@maxal.com.mx", correos, "Complemento de pago", mailHTML, attachmentPaths);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }


            return new Response<bool>(true);
        }

        public async Task<Response<bool>> sendFactura(int Id)
        {
            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarFactura.html")).ToString();
            
            var factura = await _repositoryAsyncFactura.GetByIdAsync(Id);

            var client = await _repositoryAsyncClient.GetByIdAsync(factura.ClientId);

            if(client == null)
            {
                throw new ApiException($"Cliente con Id ${factura.ClientId} en existe en clientes");
            }

            if(client.Correos == null || client.Correos.Equals(""))
            {
                throw new ApiException($"Cliente no cuenta con correo registrado");
            }

            var correos = client.Correos.Split(";");

            var attachmentPaths = new List<string>();

            var pdf = await _pdfService.PdfFactura(factura.Id);
            var pdfPath = Path.Combine(@"C:\", pdf.Data.SourcePdf);

            attachmentPaths.Add(pdfPath);

            if (factura.FileXmlTimbrado != null && !factura.FileXmlTimbrado.Equals(""))
            {
                attachmentPaths.Add(Path.Combine(@"C:\", factura.FileXmlTimbrado));
            }

            try
            {
                SendEmailWithAttachment("facturacion@maxal.com.mx",correos,"Facturación",mailHTML, attachmentPaths);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }


            return new Response<bool>(true);
        }

        public async Task<Response<bool>> sendReembolso(string nombre, int ReembolsoId, string[] ListaCorreos)
        {
            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarReembolso.html")).ToString();

            mailHTML = mailHTML.Replace("#Empleado#", nombre);
            mailHTML = mailHTML.Replace("#ReembolsoId#", ReembolsoId.ToString());

            
            try
            {
                SendEmailWithAttachment("facturacion@maxal.com.mx", ListaCorreos, "Reembolsos operativos", mailHTML, null);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return new Response<bool>(true);
        }

        public async Task<Response<bool>> sendPagoReembolso(int ReembolsoId, string correoPersonal)
        {
            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaPagoReembolso.html")).ToString();

            mailHTML = mailHTML.Replace("#ReembolsoId#", ReembolsoId.ToString());

            string[] correo = { correoPersonal,
                    "erika.aldama@solucionesintegrales-mex.com.mx",
                    "arturo.pazgo@gmail.com"
            };
            try
            {
                SendEmailWithAttachment("facturacion@maxal.com.mx", correo , "Pago reembolso operativo", mailHTML, null);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return new Response<bool>(true);
        }

        public async Task<Response<bool>> sendTicketEmail(Ticket Ticket, string correoPersonal)
        {
            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarTicket.html")).ToString();

            mailHTML = mailHTML.Replace("#TicketId#", Ticket.Id.ToString());
            mailHTML = mailHTML.Replace("#Sistema#", Ticket.Sistema.Nombre.ToString());
            mailHTML = mailHTML.Replace("#Descripcion#", Ticket.Descripcion.ToString());
            mailHTML = mailHTML.Replace("#Asignado#", Ticket.EmployeeAsignado.NombreCompleto());

            if (Ticket.TipoSolicitudTicketId == 1)
            {
                mailHTML = mailHTML.Replace("#Tipo#", "Nuevo");
            } else
            {
                mailHTML = mailHTML.Replace("#Tipo#", "Modificacion");
            }
            

            string[] correo = {correoPersonal, "erika.aldama@solucionesintegrales-mex.com.mx", "arturo.pazgo@gmail.com" };
            try
            {
                SendEmailWithAttachment("facturacion@maxal.com.mx", correo, "Centro de ayuda", mailHTML, null);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return new Response<bool>(true);
        }

        public async Task<Response<bool>> sendNominaEmail(int NominaId)
        {
            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarNomina.html")).ToString();

            var nomina = await _repositoryAsyncNomina.GetByIdAsync(NominaId);

            var employee = await _repositoryAsyncEmployee.GetByIdAsync(nomina.EmployeeId);

            if (employee == null)
            {
                throw new ApiException($"Cliente con Id 4 no existe en clientes.");
            }

            var correo = employee.MailCorporativo.Split(";");

            if (correo == null || correo.Equals(""))
            {
                throw new ApiException($"Cliente no cuenta con correo registrado");
            }

            // var correos = employee.Correos.Split(";");

            var attachmentPaths = new List<string>();

            var pdf = await _pdfService.PdfNomina(nomina.Id);
            var pdfPath = Path.Combine(@"C:\", pdf.Data.SourcePdf);

            attachmentPaths.Add(pdfPath);

            if (nomina.FileXmlTimbrado != null && !nomina.FileXmlTimbrado.Equals(""))
            {
                attachmentPaths.Add(Path.Combine(@"C:\", nomina.FileXmlTimbrado));
            }

            try
            {
                SendEmailWithAttachment("facturacion@maxal.com.mx", correo, "Recibo de nómina", mailHTML, attachmentPaths);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return new Response<bool>(true);
        }

        public async Task<Response<bool>> SendEstadoDeCuentaAhorroWise(int employeeId, int periodo)
        {
            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarEstadoDeCuentaAhorroWise.html")).ToString();

            var employee = await _repositoryAsyncEmployee.GetByIdAsync(employeeId);

            if (employee == null)
            {
                throw new ApiException($"Empleado con Id {employeeId} no existe en clientes.");
            }

            var correo = employee.MailCorporativo.Split(";");

            if (correo == null || correo.Equals(""))
            {
                throw new ApiException($"El empleado no cuenta con correo registrado.");
            }

            // var correos = employee.Correos.Split(";");

            var attachmentPaths = new List<string>();

            // var pdf = await _pdfService.PdfNomina(nomina.Id);
            // var pdfPath = Path.Combine(@"C:\", pdf.Data.SourcePdf);

            // attachmentPaths.Add(pdfPath);

            try
            {
                SendEmailWithAttachment("facturacion@maxal.com.mx", correo, "Estado de cuenta WISE periodo " + periodo, mailHTML, attachmentPaths);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return new Response<bool>(true);
        }

        public void SendEmailWithAttachment(string senderEmail, string[] recipientEmails, string subject, string body, List<string> attachmentPaths)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(senderEmail));
            foreach (var recipientEmail in recipientEmails)
            {
                message.To.Add(MailboxAddress.Parse(recipientEmail));
            }

            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = body;

            if (attachmentPaths != null)
            {

                foreach (var attachmentPath in attachmentPaths)
                {
                    builder.Attachments.Add(attachmentPath);
                }
            }

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.1and1.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("facturacion@maxal.com.mx", "Facturacion_2018");
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }

    

}


