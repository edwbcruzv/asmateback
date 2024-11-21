using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Specifications.Facturas;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.ComplementoPagoFacturas.Queries.GetAllComplementoPagoFacturaByComplementoPago
{
    public class GetAllComplementoPagoFacturaByComplementoPagoQuery : IRequest<Response<List<ComplementoPagoFacturaDto>>>
    {
        public int Id { set; get; }

        public class Handler : IRequestHandler<GetAllComplementoPagoFacturaByComplementoPagoQuery, Response<List<ComplementoPagoFacturaDto>>>
        {
            private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryAsyncComplementoPagoFactura;
            private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
            private readonly IMapper _mapper;
            private readonly IPdfService _pdfService;

            public Handler(IRepositoryAsync<ComplementoPagoFactura> repositoryAsyncComplementoPago, IMapper mapper,
                IRepositoryAsync<Factura> repositoryAsyncFactura, IPdfService pdfService)
            {
                _repositoryAsyncComplementoPagoFactura = repositoryAsyncComplementoPago;
                _mapper = mapper;
                _repositoryAsyncFactura = repositoryAsyncFactura;
                _pdfService = pdfService;
            }

            public async Task<Response<List<ComplementoPagoFacturaDto>>> Handle(GetAllComplementoPagoFacturaByComplementoPagoQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsyncComplementoPagoFactura.ListAsync( new ComplementoPagoFacturaByComplementoPagoSpecification(request.Id)  );

                var listDto = new List<ComplementoPagoFacturaDto>();
                foreach (var item  in list)
                {
                    var complementoPagoFacturaDto = _mapper.Map<ComplementoPagoFacturaDto>(item);

                    var factura = await _repositoryAsyncFactura.GetByIdAsync(item.FacturaId);

                    complementoPagoFacturaDto.FileXmlTimbrado = factura?.FileXmlTimbrado;
                    complementoPagoFacturaDto.ReceptorRfc = factura.ReceptorRfc;
                    complementoPagoFacturaDto.ReceptorRazonSocial = factura.ReceptorRazonSocial;
                    var pdfFactura = await _pdfService.PdfFactura(factura.Id);
                    complementoPagoFacturaDto.FilePdf = pdfFactura.Data.SourcePdf;

                    listDto.Add(complementoPagoFacturaDto);

                }                

                return new Response<List<ComplementoPagoFacturaDto>>(listDto);
            }
        }
    }
}
