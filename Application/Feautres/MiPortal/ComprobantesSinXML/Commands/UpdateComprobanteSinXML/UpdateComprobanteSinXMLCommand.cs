using Application.Feautres.MiPortal.Comprobantes.Commands.UpdateComprobanteCommand;
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

namespace Application.Feautres.MiPortal.ComprobantesSinXML.Commands.UpdateComprobanteSinXML
{
    public class UpdateComprobanteSinXMLCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int? ViaticoId { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public string? Concepto { get; set; }
        public IFormFile? PDF { get; set; }

        public string? Uuid { get; set; }
        public string? Folio { get; set; }
        public string? EmisorRFC { get; set; }
        public string? EmisorNombre { get; set; }
        public string? ReceptorRFC { get; set; }
        public string? ReceptorNombre { get; set; }

        public int? TipoMonedaId { get; set; }
        public int? FormaPagoId { get; set; }

        public float? SubTotal { get; set; }
        public float? Descuento { get; set; }
        public float? Total { get; set; }

        public class Handler : IRequestHandler<UpdateComprobanteSinXMLCommand, Response<int>>
        {
            private readonly IRepositoryAsync<ComprobanteSinXML> _repositoryAsyncComprobanteSinXML;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IFilesManagerService _filesManagerServiceXML;
            private readonly IXmlService _xmlService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<ComprobanteSinXML> repositoryAsyncComprobanteSinXML, IFilesManagerService filesManagerServicePDF, IFilesManagerService filesManagerServiceXML, IXmlService xmlService, IMapper mapper)
            {
                _repositoryAsyncComprobanteSinXML = repositoryAsyncComprobanteSinXML;
                _filesManagerServicePDF = filesManagerServicePDF;
                _filesManagerServiceXML = filesManagerServiceXML;
                _xmlService = xmlService;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(UpdateComprobanteSinXMLCommand request, CancellationToken cancellationToken)
            {
                var comprobante = await _repositoryAsyncComprobanteSinXML.GetByIdAsync(request.Id);

                if (comprobante == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        
                        comprobante.ViaticoId = request.ViaticoId ?? comprobante.ViaticoId;
                        comprobante.Total = request.Total ?? comprobante.Total;
                        comprobante.FechaMovimiento = request.FechaMovimiento ?? comprobante.FechaMovimiento;
                        comprobante.Concepto = request.Concepto ?? comprobante.Concepto;
                        comprobante.Uuid = request.Uuid ?? comprobante.Uuid;
                        comprobante.Folio = request.Folio ?? comprobante.Folio;
                        comprobante.EmisorRFC = request.EmisorRFC ?? comprobante.EmisorRFC;
                        comprobante.EmisorNombre = request.EmisorNombre ?? comprobante.EmisorNombre;
                        comprobante.ReceptorRFC = request.ReceptorRFC ?? comprobante.ReceptorRFC;
                        comprobante.ReceptorNombre = request.ReceptorNombre ?? comprobante.ReceptorNombre;

                        comprobante.TipoMonedaId = request.TipoMonedaId ?? comprobante.TipoMonedaId;
                        comprobante.FormaPagoId = request.FormaPagoId ?? comprobante.FormaPagoId;

                        comprobante.SubTotal = request.SubTotal ?? comprobante.SubTotal;
                        comprobante.Descuento = request.Descuento ?? comprobante.Descuento;

                        if (request.PDF != null)
                        {
                            _filesManagerServicePDF.UpdateFile(request.PDF, comprobante.PathPDF);
                        }

                        await _repositoryAsyncComprobanteSinXML.UpdateAsync(comprobante);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al asignar nuevo viatico." + ex.ToString());
                    }

                    return new Response<int>(comprobante.Id);
                }
            }
        }


    }
}
