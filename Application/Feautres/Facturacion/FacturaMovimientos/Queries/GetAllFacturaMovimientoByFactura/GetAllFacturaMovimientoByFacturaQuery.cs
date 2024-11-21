using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Specifications.Facturas;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.FacturaMovimientos.Queries.GetAllFacturaMovimientoByFactura
{
    public class GetAllFacturaMovimientoByFacturaQuery : IRequest<Response<List<FacturaMovimientoDto>>>
    {
        public int FacturaId { set; get; }

        public class Handler : IRequestHandler<GetAllFacturaMovimientoByFacturaQuery, Response<List<FacturaMovimientoDto>>>
        {
            private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsyncFactura;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<FacturaMovimiento> repositoryAsyncFactura, IMapper mapper)
            {
                _repositoryAsyncFactura = repositoryAsyncFactura;
                _mapper = mapper;
            }

            public async Task<Response<List<FacturaMovimientoDto>>> Handle(GetAllFacturaMovimientoByFacturaQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsyncFactura.ListAsync(new FacturaMovimientoByFacturaSpecification(request.FacturaId));

                var facturasDto = _mapper.Map<List<FacturaMovimientoDto>>(list);

                return new Response<List<FacturaMovimientoDto>>(facturasDto);
            }
        }
    }
}
