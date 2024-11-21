using Application.Exceptions;
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

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByFacturaSinXML
{
    public class CreateMovimientoReembolsoByFacturaSinXMLCommand : IRequest<Response<int>>
    {
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
        public int ReembolsoId { get; set; }
        public int TipoComprobanteId { get; set; }
        public int FormaPagoId { get; set; }
        public int MetodoPagoId { get; set; }

        public double Subtotal { get; set; }
        public double Total { get; set; }

        public IFormFile PDFMovReembolso { get; set; }

        public class Handler : IRequestHandler<CreateMovimientoReembolsoByFacturaSinXMLCommand, Response<int>>
        {

            private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso,
                            IRepositoryAsync<Reembolso> repositoryAsyncReembolso,
                            IRepositoryAsync<Company> repositoryAsyncCompany,
                            IFilesManagerService filesManagerServicePDF,
                            IMapper mapper)
            {
                _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolso;
                _repositoryAsyncReembolso = repositoryAsyncReembolso;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _filesManagerServicePDF = filesManagerServicePDF;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateMovimientoReembolsoByFacturaSinXMLCommand request, CancellationToken cancellationToken)
            {
                MovimientoReembolso nuevo_mov_reembolso = _mapper.Map<MovimientoReembolso>(request);
                var file_pdf = _filesManagerServicePDF.saveMovimientoReembolsoPdf(request.PDFMovReembolso, request.ReembolsoId);

                try
                {

                    // Se valido en el Validator
                    var reembolso_temp = await _repositoryAsyncReembolso.GetByIdAsync(request.ReembolsoId);
                    nuevo_mov_reembolso.PDFSrcFile = file_pdf;
                    nuevo_mov_reembolso.TipoReembolsoId = 3;

                    // validando que las companias coincidad
                    var company_temp = await _repositoryAsyncCompany.GetByIdAsync(reembolso_temp.CompanyId);
                    if (company_temp != null && company_temp.Rfc.Equals(nuevo_mov_reembolso.ReceptorRFC))
                    {

                    }
                    else
                    {
                        throw new ApiException("La Compania del Reembolso no coincide con el RFC del emisor en el movimiento.");
                    }

                    // validando que no se repita el uuid nuevo movimiento
                    var mov_reembolso_temp = await _repositoryAsyncMovimientoReembolso.FirstOrDefaultAsync(new MovimientoReembolsoByUuidSpecification(nuevo_mov_reembolso.Uuid));
                    if (mov_reembolso_temp != null)
                    {
                        return new Response<int>($"Ya existe el reembolso con Uuid {nuevo_mov_reembolso.Uuid}");
                    }
                    //Guardando en la db el nuevo movimiento
                    nuevo_mov_reembolso.TipoCambio = 1.0;
                    var data = await _repositoryAsyncMovimientoReembolso.AddAsync(nuevo_mov_reembolso);

                    //return new Response<int>("Good :D");
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
