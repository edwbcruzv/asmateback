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

namespace Application.Feautres.MiPortal.MovimientosPrestamo.Queries.GetAllMovimientosPrestamo
{
    public class GetAllMovimientosPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdQuery : IRequest<Response<List<MovimientoPrestamoDTO>>>
    {
        public int PrestamoId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }

        public class Handler : IRequestHandler<GetAllMovimientosPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdQuery, Response<List<MovimientoPrestamoDTO>>>
        {
            private readonly IRepositoryAsync<MovimientoPrestamo> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoPrestamo> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<MovimientoPrestamoDTO>>> Handle(GetAllMovimientosPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdSpecification(request.CompanyId, request.EmployeeId, request.PrestamoId));

                var list_dto = _mapper.Map<List<MovimientoPrestamoDTO>>(list);

                return new Response<List<MovimientoPrestamoDTO>>(list_dto);
            }
        }
    }
}
