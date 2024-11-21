using Application.Interfaces;
using Application.Specifications.Employees;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.SendPagoReembolsoCommand
{
    public class SendPagoReembolsoCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class Handler: IRequestHandler<SendPagoReembolsoCommand,Response<bool>>
        {
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly ISendMailService _sendMail;


            public Handler(IRepositoryAsync<Reembolso> repositoryAsyncReembolso, IRepositoryAsync<Employee> repositoryAsyncEmployee, ISendMailService sendMail)
            {
                _repositoryAsyncReembolso = repositoryAsyncReembolso;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _sendMail = sendMail;
            }

            public async Task<Response<bool>> Handle(SendPagoReembolsoCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsyncReembolso.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Reembolso no encontrado con el id {request.Id}.");
                }

                var employee = await _repositoryAsyncEmployee.FirstOrDefaultAsync(new EmployeeByUserIdSpecification(elem.UsuarioIdPago));

                if (employee == null)
                {
                    throw new KeyNotFoundException($"No hay ningún empleado asociado con el id de usuario {elem.UsuarioIdPago}.");
                }

                int reembolsoId = request.Id;

                string correoPersonal = employee.Mail;

                if (correoPersonal == null || correoPersonal == "")
                {
                    correoPersonal = employee.MailCorporativo;
                }

                Response<bool> response = await _sendMail.sendPagoReembolso(reembolsoId,correoPersonal);
                return response;
            }
        }
    }
}
