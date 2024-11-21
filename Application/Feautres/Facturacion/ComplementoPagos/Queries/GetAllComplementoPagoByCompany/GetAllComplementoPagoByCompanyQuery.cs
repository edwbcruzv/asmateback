using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Specifications.Facturas;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.ComplementoPagos.Queries.GetAllComplementoPagoByCompany
{
    public class GetAllComplementoPagoByCompanyQuery : IRequest<Response<List<ComplementoPagoDto>>>
    {
        public int CompanyId { set; get; }

        public class Handler : IRequestHandler<GetAllComplementoPagoByCompanyQuery, Response<List<ComplementoPagoDto>>>
        {
            private readonly IRepositoryAsync<ComplementoPago> _repositoryAsyncComplementoPago;
            private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryComplementoPagoFactura;
            private readonly IMapper _mapper;


            public Handler(IRepositoryAsync<ComplementoPago> repositoryAsyncComplementoPago,
                    IMapper mapper,
                    IRepositoryAsync<ComplementoPagoFactura> repositoryComplementoPagoFactura)
            {
                _repositoryAsyncComplementoPago = repositoryAsyncComplementoPago;
                _mapper = mapper;
                _repositoryComplementoPagoFactura = repositoryComplementoPagoFactura;
            }

            public async Task<Response<List<ComplementoPagoDto>>> Handle(GetAllComplementoPagoByCompanyQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsyncComplementoPago.ListAsync( new ComplementoPagoByCompanySpecification(request.CompanyId) );

                var ComplementoPagosDto = new List<ComplementoPagoDto>();

                foreach(var cp in list)
                {
                    var complemento = _mapper.Map<ComplementoPagoDto>(cp);

                    var facturasAsociadas = await _repositoryComplementoPagoFactura.ListAsync(new ComplementoPagoFacturaByComplementoPagoSpecification(cp.Id));

                    foreach (var temp in facturasAsociadas)
                    {
                        complemento.MontoTotal += temp.Monto;
                    }

                    ComplementoPagosDto.Add(complemento);

                }

                return new Response<List<ComplementoPagoDto>>(ComplementoPagosDto);
            }
        }
    }
}
