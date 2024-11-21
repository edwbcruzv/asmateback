using Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByFacturaSinXML;
using Application.Interfaces;
using Application.Specifications.ReembolsosOperativos.MovimientoReembolsos;
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

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByPagoImpuestos
{
    public class CreateMovimientoReembolsoByPagoImpuestosCommand : IRequest<Response<int>>
    {
        public string EmisorNombre { get; set; }
        public double Total { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int AnoyMes { get; set; }
        public string Concepto { get; set; }
        public int MetodoPagoId { get; set; }
        public int TipoImpuestoId { get; set; }
        public string LineaCaptura { get; set; }
        public int ReembolsoId { get; set; }
        public IFormFile PDFMovReembolso { get; set; }

        public class Handler : IRequestHandler<CreateMovimientoReembolsoByPagoImpuestosCommand, Response<int>>
        {
            private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso,
                            IRepositoryAsync<Reembolso> repositoryAsyncReembolso,
                            IFilesManagerService filesManagerServicePDF,
                            IMapper mapper)
            {
                _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolso;
                _repositoryAsyncReembolso = repositoryAsyncReembolso;
                _filesManagerServicePDF = filesManagerServicePDF;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateMovimientoReembolsoByPagoImpuestosCommand request, CancellationToken cancellationToken)
            {
                var nuevo_mov_reembolso = _mapper.Map<MovimientoReembolso>(request);
                var file_pdf = _filesManagerServicePDF.saveMovimientoReembolsoPdf(request.PDFMovReembolso, request.ReembolsoId);

                try
                {
                    // validando que no se repita el nuevo movimiento
                    var mov_reembolso_temp = await _repositoryAsyncMovimientoReembolso.FirstOrDefaultAsync(new MovimientoReembolsoByLineaCapturaSpecifiction(nuevo_mov_reembolso.LineaCaptura));
                    if (mov_reembolso_temp != null)
                    {
                        return new Response<int>($"Ya existe el reembolso con la linea de captura {nuevo_mov_reembolso.LineaCaptura}");
                    }
                    nuevo_mov_reembolso.PDFSrcFile = file_pdf;
                    nuevo_mov_reembolso.TipoReembolsoId = 5;


                    //Guardando en la db el nuevo movimiento
                    nuevo_mov_reembolso.TipoMonedaId = 115;
                    nuevo_mov_reembolso.TipoCambio = 1.0;
                    var data = await _repositoryAsyncMovimientoReembolso.AddAsync(nuevo_mov_reembolso);

                    
                    return new Response<int>(data.Id);
                }
                catch (Exception ex)
                {
                    return new Response<int>("Error: " + ex.Message);
                }
            }
        }

    }
}
