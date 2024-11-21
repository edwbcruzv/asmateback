using Application.Exceptions;
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

namespace Application.Feautres.Facturacion.Facturas.Commands.PagarFacturaCommand
{
    public class PagarFacturaCommand : IRequest<Response<int>>
    {

        public int Id { get; set; }
        public DateTime FechaPago { get; set; }
        public IFormFile ComprobantePago { get; set; }

    }
    public class Handler : IRequestHandler<PagarFacturaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsync;
        private readonly IFilesManagerService _filesManagerService;

        public Handler(IRepositoryAsync<Factura> repositoryAsync, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _filesManagerService = filesManagerService;
        }

        public async Task<Response<int>> Handle(PagarFacturaCommand request, CancellationToken cancellationToken)
        {

            var item = await _repositoryAsync.GetByIdAsync(request.Id);

            if (item == null)
            {
                throw new KeyNotFoundException($"Factura con Id: {request.Id} no encontrado en Facturas");
            }

            item.PagoSrcPdf = _filesManagerService.saveFacturaPagoPdf(request.ComprobantePago, request.Id);
            item.FechaPago = request.FechaPago;

            await _repositoryAsync.UpdateAsync(item);

            return new Response<int>(item.Id);

        }
    }
}
