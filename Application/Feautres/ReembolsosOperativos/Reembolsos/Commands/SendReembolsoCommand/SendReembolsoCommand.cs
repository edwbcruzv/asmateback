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

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.SendReembolsoCommand
{
    public class SendReembolsoCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<SendReembolsoCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Reembolso> _repositoryAsync;
        private readonly IMapper _mapper;
        private readonly ISendMailService _sendMail;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

        public Handler(IRepositoryAsync<Reembolso> repositoryAsync, IMapper mapper, ISendMailService sendMail, IRepositoryAsync<Employee> repositoryAsyncEmployee)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _sendMail = sendMail;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
        }



        public async Task<Response<bool>> Handle(SendReembolsoCommand request, CancellationToken cancellationToken)
        {
            var elem = await _repositoryAsync.GetByIdAsync(request.Id);
            if (elem == null)
            {
                throw new KeyNotFoundException($"Reembolso no encontrado con el id {request.Id}.");
            }

            var employee = await _repositoryAsyncEmployee.FirstOrDefaultAsync(new EmployeeByUserIdSpecification(elem.UsuarioIdPago));

            if (employee == null)
            {
                throw new KeyNotFoundException($"No hay ningún empleado asociado con el id de usuario {elem.UsuarioIdPago}.");
            }

            var jefe = await _repositoryAsyncEmployee.GetByIdAsync(employee.JefeId);

            if (jefe == null)
            {
                throw new KeyNotFoundException($"El empleado asosiciado al reembolso no tiene ningún jefe registrado.");
            }

            int reembolsoId = request.Id;

            string nombre = employee.ApellidoPaterno.Trim() + " " + employee.ApellidoMaterno.Trim() + " " + employee.Nombre.Trim();

            string[] listaCorreos = new string[3];

            listaCorreos[0] = employee.MailCorporativo; // Correo  del empleado
            listaCorreos[1] = jefe.MailCorporativo; // Correo del jefe
            listaCorreos[2] = "desarrollo_3@maxal.com.mx"; // Aquí es donde iría el correo de Erika

            Response<bool> response = await _sendMail.sendReembolso(nombre,reembolsoId, listaCorreos);
            return response;
        }
    }
}
