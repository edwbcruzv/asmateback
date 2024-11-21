using Application.DTOs.MiPortal.Prestamos;
using Application.Interfaces;
using Application.Specifications.MiPortal.Prestamos;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.MovimientosPrestamo.Queries.GetMovimientoPrestamoById
{
    public class GetMovimientoPrestamoByIdQuery : IRequest<Response<MovimientoPrestamoDTO>>
    {
        public int PrestamoId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }

        public class Handler : IRequestHandler<GetMovimientoPrestamoByIdQuery, Response<MovimientoPrestamoDTO>>
        {
            private readonly IRepositoryAsync<MovimientoPrestamo> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoPrestamo> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoPrestamoDTO>> Handle(GetMovimientoPrestamoByIdQuery request, CancellationToken cancellationToken)
            {

                var elem = await _repositoryAsync.GetBySpecAsync(new MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdAndMovimientoIdSpecification(request.CompanyId, request.EmployeeId, request.PrestamoId, request.MovimientoId));

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el company_id {request.CompanyId}, empleado_id {request.EmployeeId}, prestamo_id {request.PrestamoId}, movimiento_id{request.MovimientoId}");
                }

                var dto = _mapper.Map<MovimientoPrestamoDTO>(elem);
                return new Response<MovimientoPrestamoDTO>(dto, "MovimientoPrestamo encontrado con exito.");
            }
        }
    }
}
