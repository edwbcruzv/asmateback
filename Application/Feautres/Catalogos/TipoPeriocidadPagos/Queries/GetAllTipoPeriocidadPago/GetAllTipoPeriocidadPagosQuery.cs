using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs.Catalogos;

namespace Application.Feautres.Catalogos.TipoPeriocidadPagos.Queries.GetAllTipoPeriocidadPago
{
    public class GetAllTipoPeriocidadPagoQuery : IRequest<Response<List<TipoPeriocidadPagoDto>>>
    {
        public class Handler : IRequestHandler<GetAllTipoPeriocidadPagoQuery, Response<List<TipoPeriocidadPagoDto>>>
        {
            private readonly IRepositoryAsync<TipoPeriocidadPago> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<TipoPeriocidadPago> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TipoPeriocidadPagoDto>>> Handle(GetAllTipoPeriocidadPagoQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync();

                var dto = _mapper.Map<List<TipoPeriocidadPagoDto>>(list);

                return new Response<List<TipoPeriocidadPagoDto>>(dto);

            }
        }
    }
}
