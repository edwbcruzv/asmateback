using Application.Interfaces;
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

namespace Application.Feautres.MiPortal.ComprobantesSinXML.Commands.CreateComprobanteSinXML
{
    public class CreateComprobanteSinXMLCommand : IRequest<Response<int>>
    {
        public int ViaticoId { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Concepto { get; set; }
        public IFormFile PDF { get; set; }

        public string Uuid { get; set; }
        public string Folio { get; set; }
        public string EmisorRFC { get; set; }
        public string EmisorNombre { get; set; }
        public string ReceptorRFC { get; set; }
        public string ReceptorNombre { get; set; }

        public int TipoMonedaId { get; set; }
        public int FormaPagoId { get; set; }

        public float SubTotal { get; set; }
        public float Descuento { get; set; }
        public float Total { get; set; }

        public class Handler : IRequestHandler<CreateComprobanteSinXMLCommand, Response<int>>
        {
            private readonly IRepositoryAsync<ComprobanteSinXML> _repositoryAsyncComprobante;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<ComprobanteSinXML> repositoryAsyncComprobante, IFilesManagerService filesManagerServicePDF, IMapper mapper)
            {
                _repositoryAsyncComprobante = repositoryAsyncComprobante;
                _filesManagerServicePDF = filesManagerServicePDF;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateComprobanteSinXMLCommand request, CancellationToken cancellationToken)
            {
                var file_pdf = _filesManagerServicePDF.saveComprobanteViaticoPdf(request.PDF, request.ViaticoId);

                var comprobante = _mapper.Map<ComprobanteSinXML>(request);
                comprobante.PathPDF = file_pdf;

                var data = await _repositoryAsyncComprobante.AddAsync(comprobante);
                return new Response<int>(data.Id);
            }
        }

    }
}
