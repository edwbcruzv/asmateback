using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Comprobantes.Commands.DeleteComprobanteCommand
{
    public class DeleteComprobanteCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteComprobanteCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Comprobante> _repositoryAsync;
            private readonly IFilesManagerService _filesManagerServicePDF;
            private readonly IFilesManagerService _filesManagerServiceXML;

            public Handler(IRepositoryAsync<Comprobante> repositoryAsync, IFilesManagerService filesManagerServicePDF, IFilesManagerService filesManagerServiceXML)
            {
                _repositoryAsync = repositoryAsync;
                _filesManagerServicePDF = filesManagerServicePDF;
                _filesManagerServiceXML = filesManagerServiceXML;
            }

            public async Task<Response<int>> Handle(DeleteComprobanteCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Comprobante no encontrado con el id {request.Id}");
                }

                if (elem.PathXML != null)
                {
                    _filesManagerServiceXML.DeleteFile(elem.PathXML);
                }else
                {
                    Console.WriteLine($"Comprobante no tiene XML.");
                }

                if (elem.PathPDF != null)
                {
                    _filesManagerServicePDF.DeleteFile(elem.PathPDF);
                }else
                {
                    Console.WriteLine($"Comprobante no tiene PDF.");
                }


                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id, "Comprobante eliminado.");
            }
        }
    }
}
