using Application.Exceptions;
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

namespace Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteCommand
{
    public class CreateComprobanteCommand : IRequest<Response<int>>
    {
        public int ViaticoId { get; set; }
        //public float Total { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Concepto { get; set; }
        public IFormFile? XML { get; set; }
        public IFormFile PDF { get; set; }

        public class Handler : IRequestHandler<CreateComprobanteCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Comprobante> _repositoryAsyncComprobante;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IFilesManagerService _filesManagerServiceXML;
            private readonly IXmlService _xmlService;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;

            public Handler(IRepositoryAsync<Comprobante> repositoryAsyncComprobante, IFilesManagerService filesManagerServicePDF, IFilesManagerService filesManagerServiceXML,
                IXmlService xmlService, IMapper mapper, IRepositoryAsync<Viatico> repositoryAsyncViatico, IRepositoryAsync<Company> repositoryAsyncCompany)
            {
                _repositoryAsyncComprobante = repositoryAsyncComprobante;
                _filesManagerServicePDF = filesManagerServicePDF;
                _filesManagerServiceXML = filesManagerServiceXML;
                _xmlService = xmlService;
                _mapper = mapper;
                _repositoryAsyncViatico = repositoryAsyncViatico;
                _repositoryAsyncCompany = repositoryAsyncCompany;
            }

            public async Task<Response<int>> Handle(CreateComprobanteCommand request, CancellationToken cancellationToken)
            {

                var file_pdf = _filesManagerServicePDF.saveComprobanteViaticoPdf(request.PDF, request.ViaticoId);
                var file_xml = _filesManagerServiceXML.saveComprobanteViaticoXml(request.XML, request.ViaticoId);

                Comprobante comprobante = await _xmlService.GetComprobanteByXML(file_xml, request);
                comprobante.PathXML = file_xml;
                comprobante.PathPDF = file_pdf;
                comprobante.TipoComprobantes = TipoComprobantes.Factura;

                var viatico = await _repositoryAsyncViatico.GetByIdAsync(request.ViaticoId);

                var company = await _repositoryAsyncCompany.GetByIdAsync(viatico.CompanyId);

                if (company != null && company.Rfc.Equals(comprobante.ReceptorRFC))
                {

                }
                else
                {
                    throw new ApiException("La Compania del Reembolso no coincide con el RFC del emisor en el comprobante.");
                }

                var comprobantesAux = await _repositoryAsyncComprobante.FirstOrDefaultAsync(new ComprobanteByUuidSpecification(comprobante.Uuid));
                

                if (comprobantesAux != null)
                {
                    return new Response<int>($"Ya existe el comprobante con Uuid {comprobante.Uuid}");
                }

                comprobante.TipoCambio = 1;
                var data = await _repositoryAsyncComprobante.AddAsync(comprobante);
                return new Response<int>(data.Id);
            }
        }


    }
}
