using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Specifications.Facturas;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.ComplementoPagos.Queries.GetComplementoPagoByIdQuery
{
    public class GetComplementoPagoByIdQuery : IRequest<Response<ComplementoPagoDto>>
    {
        public int Id { set; get; }

        public class Handler : IRequestHandler<GetComplementoPagoByIdQuery, Response<ComplementoPagoDto>>
        {
            private readonly IRepositoryAsync<ComplementoPago> _repositoryAsyncComplementoPago;
            private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryComplementoPagoFactura;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<ComplementoPago> repositoryAsyncComplementoPago,
                IRepositoryAsync<ComplementoPagoFactura> repositoryComplementoPagoFactura, IMapper mapper)
            {
                _repositoryAsyncComplementoPago = repositoryAsyncComplementoPago;
                _repositoryComplementoPagoFactura = repositoryComplementoPagoFactura;
                _mapper = mapper;
            }

            public async Task<Response<ComplementoPagoDto>> Handle(GetComplementoPagoByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsyncComplementoPago.GetByIdAsync(request.Id);

                var ComplementoPagosDto = _mapper.Map<ComplementoPagoDto>(item);

                var facturasAsociadas = await _repositoryComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByComplementoPagoSpecification(request.Id));

                foreach (var temp in facturasAsociadas)
                {
                    ComplementoPagosDto.MontoTotal += temp.Monto;
                }

                return new Response<ComplementoPagoDto>(ComplementoPagosDto);
            }
        }
    }
}
