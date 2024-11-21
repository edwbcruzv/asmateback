using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.SendPagoReembolsoCommand;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Tickets.Commands.SendTicket
{
    public class SendTicketCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<SendTicketCommand, Response<bool>>
        {
            private readonly ISendMailService _sendMail;
            private readonly IRepositoryAsync<Ticket> _repositoryAsync;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IRepositoryAsync<Sistema> _repositoryAsyncSistema;
            public Handler(ISendMailService sendMail, IRepositoryAsync<Ticket> repositoryAsync, IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<Sistema> repositoryAsyncSistema)
            {
                _sendMail = sendMail;
                _repositoryAsync = repositoryAsync;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _repositoryAsyncSistema = repositoryAsyncSistema;
            }

            public async Task<Response<bool>> Handle(SendTicketCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var employee = await _repositoryAsyncEmployee.GetByIdAsync(elem.EmployeeAsignadoId);
                elem.EmployeeAsignado = employee;

                var sistema = await _repositoryAsyncSistema.GetByIdAsync(elem.SistemaId);
                elem.Sistema = sistema;

                string correoPersonal = employee.Mail;

                Response<bool> response = await _sendMail.sendTicketEmail(elem, correoPersonal);
                return response;
            }
        }
    }
}
