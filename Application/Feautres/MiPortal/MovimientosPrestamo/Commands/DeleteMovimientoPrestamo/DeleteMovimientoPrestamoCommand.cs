using Application.Interfaces;
using Application.Specifications.MiPortal.Prestamos;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.MovimientosPrestamo.Commands.DeleteMovimientoPrestamo
{
    public class DeleteMovimientoPrestamoCommand : IRequest<Response<int>>
    {
        public int PrestamoId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }

        public class Handler : IRequestHandler<DeleteMovimientoPrestamoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<MovimientoPrestamo> _repositoryAsync;

            public Handler(IRepositoryAsync<MovimientoPrestamo> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteMovimientoPrestamoCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetBySpecAsync(new MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdAndMovimientoIdSpecification( request.CompanyId, request.EmployeeId, request.PrestamoId, request.MovimientoId));

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el company_id {request.CompanyId}, empleado_id {request.EmployeeId}, prestamo_id {request.PrestamoId}, movimiento_id{request.MovimientoId}");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.MovimientoId, "Registro eliminado");
            }
        }
    }
}
