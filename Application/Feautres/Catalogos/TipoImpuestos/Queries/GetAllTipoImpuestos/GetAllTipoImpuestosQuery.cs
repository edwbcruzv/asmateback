using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoImpuestos.Queries.GetAllTipoImpuestos
{
    public class GetAllTipoImpuestosQuery : IRequest<Response<List<TipoImpuestoDto>>>
    {
        public class Handler : IRequestHandler<GetAllTipoImpuestosQuery, Response<List<TipoImpuestoDto>>>
        {
            private readonly IRepositoryAsync<TipoImpuesto> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoImpuesto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TipoImpuestoDto>>> Handle(GetAllTipoImpuestosQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();
                var dto = _mapper.Map<List<TipoImpuestoDto>>(list);
                return new Response<List<TipoImpuestoDto>>(dto);
            }
        }
    }
}
