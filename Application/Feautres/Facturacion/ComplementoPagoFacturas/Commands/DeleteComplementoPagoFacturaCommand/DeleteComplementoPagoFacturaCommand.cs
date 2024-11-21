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

namespace Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.DeleteComplementoPagoFacturaCommand
{
    public class DeleteComplementoPagoFacturaCommand : IRequest<Response<int>>
    {

        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<DeleteComplementoPagoFacturaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<ComplementoPagoFactura> _repositoryAsync;


        public Handler(IRepositoryAsync<ComplementoPagoFactura> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        
        }

        public async Task<Response<int>> Handle(DeleteComplementoPagoFacturaCommand request, CancellationToken cancellationToken)
        {

            var cp = await _repositoryAsync.GetByIdAsync(request.Id);

            if(cp == null)
            {
                throw new KeyNotFoundException($"ComplementoPagoFactura no encontrado con el id {request.Id}");
            }

            await _repositoryAsync.DeleteAsync(cp);

            return new Response<int>(cp.Id);


        }
    }
}
