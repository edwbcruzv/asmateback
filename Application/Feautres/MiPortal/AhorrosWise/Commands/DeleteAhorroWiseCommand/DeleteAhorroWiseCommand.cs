using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosWise.Commands.DeleteAhorroWiseCommand
{
    public class DeleteAhorroWiseCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteAhorroWiseCommand, Response<int>>
        {
            private readonly IRepositoryAsync<AhorroWise> _repositoryAsync;
            private readonly IFilesManagerService _filesManagerService;

            public Handler(IRepositoryAsync<AhorroWise> repositoryAsync, IFilesManagerService filesManagerService)
            {
                _repositoryAsync = repositoryAsync;
                _filesManagerService = filesManagerService;
            }

            public async Task<Response<int>> Handle(DeleteAhorroWiseCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                if (elem.SrcFileConstancia != null)
                {
                    _filesManagerService.DeleteFile(elem.SrcFileConstancia);
                }
                else
                {
                    Console.WriteLine($"Comprobante no tiene constancia.");
                }

                if (elem.SrcFilePago != null)
                {
                    _filesManagerService.DeleteFile(elem.SrcFilePago);
                }
                else
                {
                    Console.WriteLine($"Comprobante no tiene pago.");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id, "Registro eliminado");
            }
        }
    }
}
