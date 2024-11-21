using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoRiesgoTrabajos.Queries.GetAllTipoRiesgoTrabajos
{
    public class GetAllTipoRiesgoTrabajosQuery : IRequest<Response<List<TipoRiesgoTrabajoDto>>>
    {
        public class Handler : IRequestHandler<GetAllTipoRiesgoTrabajosQuery, Response<List<TipoRiesgoTrabajoDto>>>
        {
            private readonly IRepositoryAsync<TipoRiesgoTrabajo> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoRiesgoTrabajo> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TipoRiesgoTrabajoDto>>> Handle(GetAllTipoRiesgoTrabajosQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<TipoRiesgoTrabajoDto>>(list);

                return new Response<List<TipoRiesgoTrabajoDto>>(dto);

            }
        }
    }
}
