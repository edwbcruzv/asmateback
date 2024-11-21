using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoComprobantes.Queries.GetAllTipoComprobante
{
    public class GetAllTipoComprobanteQuery : IRequest<Response<List<TipoComprobanteDto>>>
    {     
        public class Handler : IRequestHandler<GetAllTipoComprobanteQuery, Response<List<TipoComprobanteDto>>>
        {
            private readonly IRepositoryAsync<TipoComprobante> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoComprobante> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TipoComprobanteDto>>> Handle(GetAllTipoComprobanteQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();
                
                var dto = _mapper.Map<List<TipoComprobanteDto>>(list);

                return new Response<List<TipoComprobanteDto>>(dto);                

            }
        }
    }
}
