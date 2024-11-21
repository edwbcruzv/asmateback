using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.Catalogos;
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
using System.Xml;
using System.Xml.Linq;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolso
{
    public class CreateMovimientoReembolsoFacturaCommand : IRequest<Response<int>>
    {
        public int ReembolsoId { get; set; }
        public string Concepto { get; set; }
        public IFormFile PDFMovReembolso { get; set; }
        public IFormFile XMLMovReembolso { get; set; }


        public class Handler : IRequestHandler<CreateMovimientoReembolsoFacturaCommand, Response<int>>
        {
            private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IFilesManagerService _filesManagerServiceXML;
            private readonly IXmlService _xmlService;
                
            public Handler(IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso,
                            IRepositoryAsync<Reembolso> repositoryAsyncReembolso,
                            IRepositoryAsync<Company> repositoryAsyncCompany,
                            IFilesManagerService filesManagerServicePDF,
                            IFilesManagerService filesManagerServiceXML,
                            IXmlService xmlService)
            {
                _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolso;
                _repositoryAsyncReembolso = repositoryAsyncReembolso;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _filesManagerServicePDF = filesManagerServicePDF;
                _filesManagerServiceXML = filesManagerServiceXML;
                _xmlService = xmlService;
            }

            public async Task<Response<int>> Handle(CreateMovimientoReembolsoFacturaCommand request, CancellationToken cancellationToken)
            {
                var file_pdf = _filesManagerServicePDF.saveMovimientoReembolsoPdf(request.PDFMovReembolso, request.ReembolsoId);
                var file_xml = _filesManagerServiceXML.saveMovimientoReembolsoXml(request.XMLMovReembolso, request.ReembolsoId);

                
                try
                {

                    // Creando el nuevo movimiento reembolso
                    MovimientoReembolso nuevo_mov_reembolso = await _xmlService.GetMovimientoReembolsoByXML(file_xml,request);
                    nuevo_mov_reembolso.PDFSrcFile = file_pdf;
                    nuevo_mov_reembolso.XMLSrcFile = file_xml;
                    nuevo_mov_reembolso.TipoReembolsoId = 1;

                    // Se valido su existencia en validator
                    var reembolso_temp = await _repositoryAsyncReembolso.GetByIdAsync(request.ReembolsoId);

                    // validando que las companias coincidan
                    var company_temp = await _repositoryAsyncCompany.GetByIdAsync(reembolso_temp.CompanyId);
                    if (company_temp != null && company_temp.Rfc.Equals(nuevo_mov_reembolso.ReceptorRFC))
                    {

                    }
                    else
                    {
                        throw new ApiException("La Compania del Reembolso no coincide con el RFC del emisor en el movimiento.");
                    }

                    // validando que no se repita el nuevo movimiento
                    var mov_reembolso_temp = await _repositoryAsyncMovimientoReembolso.FirstOrDefaultAsync(new MovimientoReembolsoByUuidSpecification(nuevo_mov_reembolso.Uuid));
                    if(mov_reembolso_temp != null)
                    {
                        return new Response<int>($"Ya existe el reembolso con Uuid {nuevo_mov_reembolso.Uuid}");
                    }
                    nuevo_mov_reembolso.TipoCambio = 1.0;
                    //Guardando en la db el nuevo movimiento
                    var data = await _repositoryAsyncMovimientoReembolso.AddAsync(nuevo_mov_reembolso);
                    

                    //return new Response<int>("Good :D");
                    return new Response<int>(data.Id);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    return new Response<int>("Error: " + ex.Message);
                }

            }

        }

    }
}
