using Application.Interfaces;
using Application.Specifications.MiPortal.Comprobantes;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteFacturaExtrangera
{
    public class CreateComprobanteFacturaExtranjeraCommand : IRequest<Response<int>>
    {
        public int ViaticoId { get; set; }
        public string Uuid { get; set; }
        public string Concepto { get; set; }

        public string EmisorRFC { get; set; }
        public string EmisorNombre { get; set; }

        public string ReceptorRFC { get; set; }
        public string ReceptorNombre { get; set; }

        public string LugarExpedicion { get; set; }
        public DateTime FechaTimbrado { get; set; }

        public double? IVATrasladados { get; set; }
        public double? IVARetenidos { get; set; }
        public double? ISR { get; set; }

        public int TipoMonedaId { get; set; }
        public float TipoCambio { get; set; }
        public int RegimenFiscalId { get; set; } // del emisor
        public int TipoComprobanteId { get; set; }
        public int FormaPagoId { get; set; }
        public int MetodoPagoId { get; set; }

        public double Subtotal { get; set; }
        public double Total { get; set; }

        public IFormFile PDF { get; set; }

        public class Handler : IRequestHandler<CreateComprobanteFacturaExtranjeraCommand, Response<int>>
        {

            private readonly IRepositoryAsync<Comprobante> _repositoryAsyncComprobante;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Comprobante> repositoryAsyncComprobante, IFilesManagerService filesManagerServicePDF, IMapper mapper)
            {
                _repositoryAsyncComprobante = repositoryAsyncComprobante;
                _filesManagerServicePDF = filesManagerServicePDF;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateComprobanteFacturaExtranjeraCommand request, CancellationToken cancellationToken)
            {
                var file_pdf = _filesManagerServicePDF.saveComprobanteViaticoPdf(request.PDF, request.ViaticoId);
                var comprobante = _mapper.Map<Comprobante>(request);

                var comprobantesAux = await _repositoryAsyncComprobante.FirstOrDefaultAsync(new ComprobanteByUuidSpecification(comprobante.Uuid));

                if (comprobantesAux != null)
                {
                    return new Response<int>($"Ya existe el comprobante con Uuid {comprobante.Uuid}");
                }

                comprobante.TipoComprobantes = TipoComprobantes.FacturaExtranjera;
                comprobante.PathPDF = file_pdf;
                comprobante.FechaMovimiento = DateTime.Now;
                
                var data = await _repositoryAsyncComprobante.AddAsync(comprobante);
                return new Response<int>(data.Id);
            }
        }
    }
}
