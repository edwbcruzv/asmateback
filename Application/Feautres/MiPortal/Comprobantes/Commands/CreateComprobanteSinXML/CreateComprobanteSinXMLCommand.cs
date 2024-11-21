using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByFacturaSinXML;
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

namespace Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteSinXML
{
    public class CreateComprobanteSinXMLCommand : IRequest<Response<int>>
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
        public double? IEPS { get; set; }
        public double? ISH { get; set; }

        public int TipoMonedaId { get; set; }
        public int RegimenFiscalId { get; set; } // del emisor
        public int TipoComprobanteId { get; set; }
        public int FormaPagoId { get; set; }
        public int MetodoPagoId { get; set; }

        public float SubTotal { get; set; }
        public float Total { get; set; }

        public IFormFile PDF { get; set; }

        public class Handler : IRequestHandler<CreateComprobanteSinXMLCommand, Response<int>>
        {

            private readonly IRepositoryAsync<Comprobante> _repositoryAsyncComprobante;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Comprobante> repositoryAsyncComprobante, IMapper mapper, IFilesManagerService filesManagerServicePDF)
            {
                _repositoryAsyncComprobante = repositoryAsyncComprobante;
                _mapper = mapper;
                _filesManagerServicePDF = filesManagerServicePDF;
            }

            public async Task<Response<int>> Handle(CreateComprobanteSinXMLCommand request, CancellationToken cancellationToken)
            {
                var file_pdf = _filesManagerServicePDF.saveComprobanteViaticoPdf(request.PDF, request.ViaticoId);
                var comprobante = _mapper.Map<Comprobante>(request);

                var comprobantesAux = await _repositoryAsyncComprobante.FirstOrDefaultAsync(new ComprobanteByUuidSpecification(comprobante.Uuid));
                if (comprobantesAux != null)
                {
                    return new Response<int>($"Ya existe el comprobante con Uuid {comprobante.Uuid}");
                }

                comprobante.TipoComprobantes = TipoComprobantes.FacturaSinXML;
                comprobante.PathPDF = file_pdf;
                comprobante.FechaMovimiento = DateTime.Now;
                comprobante.TipoCambio = 1;
                var data = await _repositoryAsyncComprobante.AddAsync(comprobante);
                return new Response<int>(data.Id);
            }
        }
    }
}
