using Application.DTOs.Kanban.Sistemas;
using Application.Interfaces;
using Application.Specifications.Kanban.Sistemas;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Sistemas.Queries.GetAllSistemas
{
    public class GetAllSistemasByEstadoIdQuery : IRequest<Response<List<SistemaDTO>>>
    {
        
        public int EstadoId { get; set; }

        public class Handler : IRequestHandler<GetAllSistemasByEstadoIdQuery, Response<List<SistemaDTO>>>
        {
            private readonly IRepositoryAsync<Sistema> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Sistema> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<SistemaDTO>>> Handle(GetAllSistemasByEstadoIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new SistemaByEstadoIdSpecification(request.EstadoId));

                var list_dto = _mapper.Map<List<SistemaDTO>>(list);

                return new Response<List<SistemaDTO>>(list_dto);
            }
        }
    }
}
