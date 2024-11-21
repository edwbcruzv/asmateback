using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoComprobantes.Queries.GetTipoComprobanteById
{
    public class GetTipoComprobanteByIdQuery : IRequest<Response<TipoComprobanteDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetTipoComprobanteByIdQuery, Response<TipoComprobanteDto>>
        {
            private readonly IRepositoryAsync<TipoComprobante> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoComprobante> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TipoComprobanteDto>> Handle(GetTipoComprobanteByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<TipoComprobanteDto>(item);

                return new Response<TipoComprobanteDto>(dto);

            }
        }
    }
}
