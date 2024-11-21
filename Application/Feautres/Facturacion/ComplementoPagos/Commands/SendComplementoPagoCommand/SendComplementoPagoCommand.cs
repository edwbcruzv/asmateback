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

namespace Application.Feautres.Facturacion.ComplementoPagos.Commands.SendComplementoPagoCommand
{
    public class SendComplementoPagoCommand : IRequest<Response<bool>>
    {

        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<SendComplementoPagoCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<ComplementoPago> _repositoryAsync;
        private readonly ISendMailService _sendMail;

        public Handler(IRepositoryAsync<ComplementoPago> repositoryAsync, ISendMailService sendMail)
        {
            _repositoryAsync = repositoryAsync;
            _sendMail = sendMail;
        }

        public async Task<Response<bool>> Handle(SendComplementoPagoCommand request, CancellationToken cancellationToken)
        {

            var item = await _repositoryAsync.GetByIdAsync(request.Id);

            if (item == null)
            {
                throw new KeyNotFoundException($"ComplementoPago con Id: {request.Id} no encontrado en ComplementoPagos");
            }

            var response = await _sendMail.sendComplementoPago(request.Id);

            return response;

        }
    }
}
