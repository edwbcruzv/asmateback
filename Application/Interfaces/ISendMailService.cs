using Application.DTOs.Facturas;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISendMailService
    {
        public Task<Response<bool>> sendFactura(int Id);
        public Task<Response<bool>> sendComplementoPago(int Id);
        public Task<Response<bool>> sendReembolso(string Nombre, int ReembolsoId, string[] ListaCorreos);
        public Task<Response<bool>> sendPagoReembolso(int ReembolsoId, string correoPersonal);
        public Task<Response<bool>> sendTicketEmail(Ticket TicketID, string correoPersonal);
        public Task<Response<bool>> sendNominaEmail(int NominaId);
        public Task<Response<bool>> SendEstadoDeCuentaAhorroWise(int employeeId, int periodo);
        public void SendEmailWithAttachment(string senderEmail, string[] recipientEmails, string subject, string body, List<string> attachmentPaths);
    }
}
