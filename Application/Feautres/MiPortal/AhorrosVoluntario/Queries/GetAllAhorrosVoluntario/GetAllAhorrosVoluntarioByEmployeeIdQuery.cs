using Application.DTOs.MiPortal.Ahorros;
using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosVoluntario;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosVoluntario.Queries.GetAllAhorrosVoluntario
{
    public class GetAllAhorrosVoluntarioByEmployeeIdQuery : IRequest<Response<List<AhorroVoluntarioDTO>>>
    {

        public int Id { get; set; }

        public class Handler : IRequestHandler<GetAllAhorrosVoluntarioByEmployeeIdQuery, Response<List<AhorroVoluntarioDTO>>>
        {
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<AhorroVoluntario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<AhorroVoluntarioDTO>>> Handle(GetAllAhorrosVoluntarioByEmployeeIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new AhorroVoluntarioByEmployeeIdSpecification(request.Id));

                var list_dto = _mapper.Map<List<AhorroVoluntarioDTO>>(list);

                return new Response<List<AhorroVoluntarioDTO>>(list_dto);
            }
        }
    }
}
