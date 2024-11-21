using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosVoluntario;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Commands.DeleteMovimientoAhorroVoluntario
{
    public class DeleteMovimientoAhorroVoluntarioCommand : IRequest<Response<int>>
    {
        public int AhorroVoluntarioId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }

        public class Handler : IRequestHandler<DeleteMovimientoAhorroVoluntarioCommand, Response<int>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsync;

            public Handler(IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteMovimientoAhorroVoluntarioCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetBySpecAsync(new MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdAndMovimientoIdSpecification(request.CompanyId, request.EmployeeId, request.AhorroVoluntarioId,  request.MovimientoId));

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el company_id {request.CompanyId},  empleado_id {request.EmployeeId}, ahorro_id {request.AhorroVoluntarioId}, movimiento_id{request.MovimientoId}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.MovimientoId, "Registro eliminado");
            }
        }
    }
}
