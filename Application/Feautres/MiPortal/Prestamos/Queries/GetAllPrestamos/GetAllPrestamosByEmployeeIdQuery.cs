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

namespace Application.Feautres.MiPortal.Prestamos.Queries.GetAllPrestamos
{
    public class GetAllPrestamosByEmployeeIdQuery : IRequest<Response<List<PrestamoDTO>>>
    {

        public int Id { get; set; }

        public class Handler : IRequestHandler<GetAllPrestamosByEmployeeIdQuery, Response<List<PrestamoDTO>>>
        {
            private readonly IRepositoryAsync<Prestamo> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Prestamo> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<PrestamoDTO>>> Handle(GetAllPrestamosByEmployeeIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new PrestamoByEmployeeIdSpecification(request.Id));

                var list_dto = _mapper.Map<List<PrestamoDTO>>(list);

                return new Response<List<PrestamoDTO>>(list_dto);
            }
        }
    }
}
