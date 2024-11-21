using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Specifications.Facturas;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.ComplementoPagoFacturas.Queries.GetComplementoPagoFacturaById
{
    public class GetComplementoPagoFacturaByIdQuery : IRequest<Response<ComplementoPagoFacturaDto>>
    {
        public int Id { set; get; }

        public class Handler : IRequestHandler<GetComplementoPagoFacturaByIdQuery, Response<ComplementoPagoFacturaDto>>
        {
            private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryAsyncComplementoPago;
            private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
            private readonly IPdfService _pdfService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<ComplementoPagoFactura> repositoryAsyncComplementoPago, IMapper mapper, IRepositoryAsync<Factura> repositoryAsyncFactura, IPdfService pdfService)
            {
                _repositoryAsyncComplementoPago = repositoryAsyncComplementoPago;
                _mapper = mapper;
                _repositoryAsyncFactura = repositoryAsyncFactura;
                _pdfService = pdfService;
            }

            public async Task<Response<ComplementoPagoFacturaDto>> Handle(GetComplementoPagoFacturaByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsyncComplementoPago.GetByIdAsync(request.Id);

                var complementoPagoFacturaDto = _mapper.Map<ComplementoPagoFacturaDto>(item);
                var factura = await _repositoryAsyncFactura.GetByIdAsync(item.FacturaId);

                complementoPagoFacturaDto.FileXmlTimbrado = factura?.FileXmlTimbrado;
                complementoPagoFacturaDto.ReceptorRfc = factura.ReceptorRfc;
                complementoPagoFacturaDto.ReceptorRazonSocial = factura.ReceptorRazonSocial;
                var pdfFactura = await _pdfService.PdfFactura(factura.Id);
                complementoPagoFacturaDto.FilePdf = pdfFactura.Data.SourcePdf;

                return new Response<ComplementoPagoFacturaDto>(complementoPagoFacturaDto);
            }
        }
    }
}
