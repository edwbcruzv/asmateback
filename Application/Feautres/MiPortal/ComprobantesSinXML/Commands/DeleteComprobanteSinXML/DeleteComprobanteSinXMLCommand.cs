using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.ComprobantesSinXML.Commands.DeleteComprobanteSinXML
{
    public class DeleteComprobanteSinXMLCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteComprobanteSinXMLCommand, Response<int>>
        {
            private readonly IRepositoryAsync<ComprobanteSinXML> _repositoryAsync;
            private readonly IFilesManagerService _filesManagerServicePDF;

            public Handler(IRepositoryAsync<ComprobanteSinXML> repositoryAsync, IFilesManagerService filesManagerServicePDF, IFilesManagerService filesManagerServiceXML)
            {
                _repositoryAsync = repositoryAsync;
                _filesManagerServicePDF = filesManagerServicePDF;
            }

            public async Task<Response<int>> Handle(DeleteComprobanteSinXMLCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"ComprobanteSinXML no encontrado con el id {request.Id}");
                }

                if (elem.PathPDF != null)
                {
                    _filesManagerServicePDF.DeleteFile(elem.PathPDF);
                }
                else
                {
                    throw new KeyNotFoundException($"ComprobanteSinXML no tiene PDF.");
                }


                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id, "ComprobanteSinXML eliminado.");
            }
        }
    }
}
