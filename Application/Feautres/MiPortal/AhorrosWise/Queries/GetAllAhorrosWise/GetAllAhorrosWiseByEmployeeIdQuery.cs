using Application.DTOs.MiPortal.Ahorros;
using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosWise;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosWise.Queries.GetAllAhorrosWise
{
    public class GetAllAhorrosWiseByEmployeeIdQuery : IRequest<Response<List<AhorroWiseDTO>>>
    {

        public int Id { get; set; }

        public class Handler : IRequestHandler<GetAllAhorrosWiseByEmployeeIdQuery, Response<List<AhorroWiseDTO>>>
        {
            private readonly IRepositoryAsync<AhorroWise> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<AhorroWise> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<AhorroWiseDTO>>> Handle(GetAllAhorrosWiseByEmployeeIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new AhorroWiseByEmployeeIdSpecification(request.Id));

                var list_dto = _mapper.Map<List<AhorroWiseDTO>>(list);

                return new Response<List<AhorroWiseDTO>>(list_dto);
            }
        }
    }
}
