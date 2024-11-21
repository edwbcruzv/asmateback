using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoJornadas.Queries.GetAllTipoJornada
{
    public class GetAllTipoJornadaQuery : IRequest<Response<List<TipoJornadaDto>>>
    {
        public class Handler : IRequestHandler<GetAllTipoJornadaQuery, Response<List<TipoJornadaDto>>>
        {
            private readonly IRepositoryAsync<TipoJornada> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoJornada> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TipoJornadaDto>>> Handle(GetAllTipoJornadaQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<TipoJornadaDto>>(list);

                return new Response<List<TipoJornadaDto>>(dto);

            }
        }
    }
}
