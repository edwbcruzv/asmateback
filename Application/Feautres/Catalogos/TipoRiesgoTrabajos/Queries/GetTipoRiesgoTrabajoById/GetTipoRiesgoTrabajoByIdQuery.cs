using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoRiesgoTrabajos.Queries.GetTipoRiesgoTrabajoById
{
    public class GetTipoRiesgoTrabajoByIdQuery : IRequest<Response<TipoRiesgoTrabajoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetTipoRiesgoTrabajoByIdQuery, Response<TipoRiesgoTrabajoDto>>
        {
            private readonly IRepositoryAsync<TipoRiesgoTrabajo> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoRiesgoTrabajo> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TipoRiesgoTrabajoDto>> Handle(GetTipoRiesgoTrabajoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<TipoRiesgoTrabajoDto>(item);

                return new Response<TipoRiesgoTrabajoDto>(dto);

            }
        }
    }
}
