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

namespace Application.Feautres.Facturacion.ComplementoPagos.Commands.DeleteComplementoPagoCommand
{
    public class DeleteComplementoPagoCommand : IRequest<Response<int>>
    {

        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<DeleteComplementoPagoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<ComplementoPago> _repositoryAsync;


        public Handler(IRepositoryAsync<ComplementoPago> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        
        }

        public async Task<Response<int>> Handle(DeleteComplementoPagoCommand request, CancellationToken cancellationToken)
        {

            var cp = await _repositoryAsync.GetByIdAsync(request.Id);

            if(cp == null)
            {
                throw new KeyNotFoundException($"ComplementoPago no encontrado con el id {request.Id}");
            }

            await _repositoryAsync.DeleteAsync(cp);

            return new Response<int>(cp.Id);


        }
    }
}
