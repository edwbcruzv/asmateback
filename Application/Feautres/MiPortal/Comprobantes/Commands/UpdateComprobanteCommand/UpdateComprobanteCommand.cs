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

namespace Application.Feautres.MiPortal.Comprobantes.Commands.UpdateComprobanteCommand
{
    public class UpdateComprobanteCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int? ViaticoId { get; set; }
        public float? Total { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public string? Concepto { get; set; }
        public IFormFile? XML { get; set; }
        public IFormFile? PDF { get; set; }


        public class Handler : IRequestHandler<UpdateComprobanteCommand , Response<int>>
        {
            private readonly IRepositoryAsync<Comprobante> _repositoryAsyncComprobante;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IFilesManagerService _filesManagerServiceXML;
            private readonly IXmlService _xmlService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Comprobante> repositoryAsyncComprobante, IFilesManagerService filesManagerServicePDF, IFilesManagerService filesManagerServiceXML, IXmlService xmlService, IMapper mapper)
            {
                _repositoryAsyncComprobante = repositoryAsyncComprobante;
                _filesManagerServicePDF = filesManagerServicePDF;
                _filesManagerServiceXML = filesManagerServiceXML;
                _xmlService = xmlService;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(UpdateComprobanteCommand request, CancellationToken cancellationToken)
            {
                var comprobante = await _repositoryAsyncComprobante.GetByIdAsync(request.Id);

                if (comprobante == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        if (request.ViaticoId !=null && request.ViaticoId != 0)
                        {
                            comprobante.ViaticoId = (int)request.ViaticoId;
                        }

                        if (request.Total != null)
                        {
                            comprobante.Total = (float)request.Total;
                        }

                        if (request.FechaMovimiento != null)
                        {
                            comprobante.FechaMovimiento = (DateTime)request.FechaMovimiento;
                        }

                        if (request.Concepto != null)
                        {
                            comprobante.Concepto = request.Concepto;
                        }

                        if (request.PDF != null)
                        {
                            _filesManagerServicePDF.UpdateFile(request.PDF, comprobante.PathPDF);
                        }

                        if (request.XML != null)
                        {
                            _filesManagerServiceXML.UpdateFile(request.XML, comprobante.PathXML);
                        }

                        await _repositoryAsyncComprobante.UpdateAsync(comprobante);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar comprobante." + ex.ToString());
                    }

                    return new Response<int>(comprobante.Id);
                }
            }
        }


    }
}
