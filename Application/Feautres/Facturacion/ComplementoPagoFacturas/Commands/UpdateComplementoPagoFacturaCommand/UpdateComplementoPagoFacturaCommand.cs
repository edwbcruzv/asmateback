using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.UpdateComplementoPagoFacturaCommand
{
    public class UpdateComplementoPagoFacturaCommand : IRequest<Response<int>>
    {

        public int Id { get; set; }
        public int ComplementoPagoId { get; set; }
        public int FacturaId { get; set; }
        public int Folio { get; set; }
        public double Monto { get; set; }

    }
    public class Handler : IRequestHandler<UpdateComplementoPagoFacturaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryAsync;
        private readonly IFilesManagerService _filesManagerService;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<ComplementoPagoFactura> repositoryAsync, IMapper mapper, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _filesManagerService = filesManagerService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateComplementoPagoFacturaCommand request, CancellationToken cancellationToken)
        {

            var cp = await _repositoryAsync.GetByIdAsync(request.Id);

            if(cp == null)
            {
                throw new KeyNotFoundException($"ComplementoPago no encontrado con el id {request.Id}");
            }

            cp.ComplementoPagoId = request.ComplementoPagoId;
            cp.FacturaId = request.FacturaId;
            cp.Folio = request.Folio;
            cp.Monto = request.Monto;

            await _repositoryAsync.UpdateAsync(cp);

            return new Response<int>(cp.Id);


        }
    }
}
