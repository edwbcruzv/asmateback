using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoContratos.Queries.GetAllTipoContrato
{
    public class GetAllTipoContratoQuery : IRequest<Response<List<TipoContratoDto>>>
    {
        public class Handler : IRequestHandler<GetAllTipoContratoQuery, Response<List<TipoContratoDto>>>
        {
            private readonly IRepositoryAsync<TipoContrato> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoContrato> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TipoContratoDto>>> Handle(GetAllTipoContratoQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<TipoContratoDto>>(list);

                return new Response<List<TipoContratoDto>>(dto);

            }
        }
    }
}
