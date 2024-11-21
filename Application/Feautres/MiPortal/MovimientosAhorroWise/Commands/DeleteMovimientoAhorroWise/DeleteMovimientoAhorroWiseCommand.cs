using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosWise;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.DeleteMovimientoAhorroWise
{
    public class DeleteMovimientoAhorroWiseCommand : IRequest<Response<int>>
    {
        public int AhorroWiseId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }

        public class Handler : IRequestHandler<DeleteMovimientoAhorroWiseCommand, Response<int>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroWise> _repositoryAsync;

            public Handler(IRepositoryAsync<MovimientoAhorroWise> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteMovimientoAhorroWiseCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetBySpecAsync(new MovimientoAhorroWiseByCompanyIdAndEmployeeIdAndAhorroWiseIdAndMovimientoIdSpecification(request.CompanyId, request.EmployeeId, request.AhorroWiseId, request.MovimientoId));

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el company_id {request.CompanyId}, empleado_id {request.EmployeeId},  ahorro_id {request.AhorroWiseId}, movimiento_id{request.MovimientoId}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.MovimientoId, "Registro eliminado");
            }
        }
    }
}
