using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoMonedas.Queries.GetAllTipoMoneda
{
    public class GetAllTipoMonedaQuery : IRequest<Response<List<TipoMonedaDto>>>
    {
        public class Handler : IRequestHandler<GetAllTipoMonedaQuery, Response<List<TipoMonedaDto>>>
        {
            private readonly IRepositoryAsync<TipoMoneda> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoMoneda> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TipoMonedaDto>>> Handle(GetAllTipoMonedaQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<TipoMonedaDto>>(list);

                return new Response<List<TipoMonedaDto>>(dto);

            }
        }
    }
}
