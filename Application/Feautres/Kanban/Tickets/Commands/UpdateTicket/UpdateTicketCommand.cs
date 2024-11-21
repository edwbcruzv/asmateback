using Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.UpdateReembolsoCommand;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int Estatus { get; set; }


        public class Handler : IRequestHandler<UpdateTicketCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Ticket> _repositoryAsyncTicket;
            private readonly IFilesManagerService _filesManagerService;

            public Handler(IRepositoryAsync<Ticket> repositoryAsyncTicket, IFilesManagerService filesManagerService)
            {
                _repositoryAsyncTicket = repositoryAsyncTicket;
                _filesManagerService = filesManagerService;
            }

            public async Task<Response<int>> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
            {
                var ticket = await _repositoryAsyncTicket.GetByIdAsync(request.Id);

                if (ticket == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        ticket.Estatus = (EstatusTicket)request.Estatus;
                        await _repositoryAsyncTicket.UpdateAsync(ticket);

                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al asignar nuevo estatus." + ex.ToString());
                    }

                    return new Response<int>(ticket.Id);

                }
            }
        }
    }
}
