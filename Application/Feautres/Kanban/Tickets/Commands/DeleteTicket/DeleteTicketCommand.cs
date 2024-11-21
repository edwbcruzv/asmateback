using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteTicketCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Ticket> _repositoryAsync;
            private readonly IFilesManagerService _filesManagerService;

            public Handler(IRepositoryAsync<Ticket> repositoryAsync, IFilesManagerService filesManagerService)
            {
                _repositoryAsync = repositoryAsync;
                _filesManagerService = filesManagerService;
            }

            public async Task<Response<int>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Ticket no encontrado con el id {request.Id}");
                }

                if (elem.SrcImagen != null)
                {
                    _filesManagerService.DeleteFile(elem.SrcImagen);
                }
                else
                {
                    Console.WriteLine($"El ticket no tiene imagen.");
                }

                await _repositoryAsync.DeleteAsync(elem);

                return new Response<int>(elem.Id);
            }
        }
    }
}
