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

namespace Application.Feautres.Facturacion.Facturas.Commands.SendFacturaCommand
{
    public class SendFacturaCommand : IRequest<Response<bool>>
    {

        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<SendFacturaCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsync;
        private readonly ISendMailService _sendMail;

        public Handler(IRepositoryAsync<Factura> repositoryAsync, ISendMailService sendMail)
        {
            _repositoryAsync = repositoryAsync;
            _sendMail = sendMail;
        }

        public async Task<Response<bool>> Handle(SendFacturaCommand request, CancellationToken cancellationToken)
        {

            var item = await _repositoryAsync.GetByIdAsync(request.Id);

            if (item == null)
            {
                throw new KeyNotFoundException($"Factura con Id: {request.Id} no encontrado en Facturas");
            }

            var response = await _sendMail.sendFactura(request.Id);

            return response;

        }
    }
}
