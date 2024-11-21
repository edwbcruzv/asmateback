using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.PagarReembolsoCommand
{
    public class PagarReembolsoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public DateTime FechaPago {  get; set; }
        public IFormFile PDFPagoComprobante { get; set; }
        public string Clabe { get; set; }


        public class Handler : IRequestHandler<PagarReembolsoCommand,Response<int>>
        {
            private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
            private readonly IFilesManagerService _filesManagerService;

            public Handler(IRepositoryAsync<Reembolso> repositoryAsyncReembolso, IFilesManagerService filesManagerService)
            {
                _repositoryAsyncReembolso = repositoryAsyncReembolso;
                _filesManagerService = filesManagerService;
            }

            public async Task<Response<int>> Handle(PagarReembolsoCommand request, CancellationToken cancellationToken)
            {

                var elem = await _repositoryAsyncReembolso.GetByIdAsync(request.Id);
                if (elem.EstatusId == 2)
                {
                    string rutaArchivo = _filesManagerService.saveReembolsoPdf(request.PDFPagoComprobante, request.Id);
                    elem.FechaPago = request.FechaPago;
                    elem.SrcPdfPagoComprobante = rutaArchivo;
                    elem.Clabe = request.Clabe;
                    elem.EstatusId = 3;

                    await _repositoryAsyncReembolso.UpdateAsync(elem);

                    Response<int> respuesta = new Response<int>();
                    respuesta.Succeeded = true;
                    respuesta.Data = elem.Id;
                    respuesta.Message = "El reembolso fue pagado con éxito";

                    return respuesta;
                }
                else
                {
                    Response<int> respuesta = new Response<int>();
                    respuesta.Succeeded = false;
                    respuesta.Message = "Solo se pueden pagar los reembolsos que están Cerrados";

                    return respuesta;

                }
                

            }
        }
    }
}
