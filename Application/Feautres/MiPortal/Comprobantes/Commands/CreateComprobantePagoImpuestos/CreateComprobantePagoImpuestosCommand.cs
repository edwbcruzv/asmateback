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

namespace Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobantePagoImpuestos
{
    public class CreateComprobantePagoImpuestosCommand : IRequest<Response<int>>
    {
        public int ViaticoId { get; set; }
        public string EmisorNombre { get; set; }
        public double Total { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int AnoyMes { get; set; }
        public string Concepto { get; set; }
        public int MetodoPagoId { get; set; }
        public int TipoImpuestoId { get; set; }
        public string LineaCaptura { get; set; }
        public IFormFile PDF { get; set; }

        public class Handler : IRequestHandler<CreateComprobantePagoImpuestosCommand, Response<int>>
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

            public async Task<Response<int>> Handle(CreateComprobantePagoImpuestosCommand request, CancellationToken cancellationToken)
            {
                var file_pdf = _filesManagerServicePDF.saveComprobanteViaticoPdf(request.PDF, request.ViaticoId);
                var comprobante = _mapper.Map<Comprobante>(request);
                comprobante.TipoComprobantes = TipoComprobantes.PagoImpuestos;
                comprobante.PathPDF = file_pdf;
                comprobante.FechaMovimiento = DateTime.Now;
                comprobante.TipoCambio = 1;
                var data = await _repositoryAsyncComprobante.AddAsync(comprobante);
                return new Response<int>(data.Id);
            }
        }
    }
}
