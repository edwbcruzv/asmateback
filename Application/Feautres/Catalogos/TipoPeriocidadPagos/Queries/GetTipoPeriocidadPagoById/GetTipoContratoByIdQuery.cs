using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoPeriocidadPagos.Queries.GetTipoPeriocidadPagoById
{
    public class GetTipoPeriocidadPagoByIdQuery : IRequest<Response<TipoPeriocidadPagoDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetTipoPeriocidadPagoByIdQuery, Response<TipoPeriocidadPagoDto>>
        {
            private readonly IRepositoryAsync<TipoPeriocidadPago> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoPeriocidadPago> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TipoPeriocidadPagoDto>> Handle(GetTipoPeriocidadPagoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                var dto = _mapper.Map<TipoPeriocidadPagoDto>(item);

                return new Response<TipoPeriocidadPagoDto>(dto);

            }
        }
    }
}
