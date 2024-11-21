using Application.Feautres.Facturacion.Facturas.Commands.CreateFacturaCommand;
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

namespace Application.Feautres.Kanban.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest<Response<int>>
    {
        public int TipoSolicitudTicketId { get; set; }
        public int CompanyId { get; set; }
        public int SistemaId { get; set; }
        public int DepartamentoId { get; set; }
        public int EmployeeAsignadoId { get; set; }
        public int EmployeeCreadorId { get; set; }
        public int EstadoId { get; set; }
        public string OpcionMenu { get; set; }
        public string? OpcionSubMenu { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public IFormFile? Imagen { get; set; }
        public Prioridad Prioridad { get; set; }

    }

    public class Handler : IRequestHandler<CreateTicketCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Ticket> _repositoryAsyncTicket;
        private readonly IFilesManagerService _filesManagerService;

        private readonly IMapper _mapper;

        public Handler(IRepositoryAsync<Ticket> repositoryAsyncTicket,
                    IMapper mapper,
                    IFilesManagerService filesManagerService)
        {
            _repositoryAsyncTicket = repositoryAsyncTicket;
            _mapper = mapper;
            _filesManagerService = filesManagerService;
        }

        public async Task<Response<int>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = _mapper.Map<Ticket>(request);
            ticket.Estatus = EstatusTicket.Abierto;
            ticket.SrcImagen = _filesManagerService.saveTicketImage(request.Imagen, ticket.Id);

            var data = await _repositoryAsyncTicket.AddAsync(ticket);
            Console.WriteLine(data);
            return new Response<int>(data.Id);
        }
    }
}
