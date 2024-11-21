using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoMonedas.Queries.GetTipoMonedaById
{
    public class GetTipoMonedaByIdQuery : IRequest<Response<TipoMonedaDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetTipoMonedaByIdQuery, Response<TipoMonedaDto>>
        {
            private readonly IRepositoryAsync<TipoMoneda> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoMoneda> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TipoMonedaDto>> Handle(GetTipoMonedaByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<TipoMonedaDto>(item);

                return new Response<TipoMonedaDto>(dto);

            }
        }
    }
}
