using Application.DTOs.Facturas;
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

namespace Application.Feautres.Facturacion.Facturas.Commands.CancelarFacturaCommand
{
    public class CancelarFacturaCommand : IRequest<Response<EstatusCancelacionDto>>
    {

        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<CancelarFacturaCommand, Response<EstatusCancelacionDto>>
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsync;
        private readonly ITimboxService _timbox;



        public Handler(IRepositoryAsync<Factura> repositoryAsync, ITimboxService timbox)
        {
            _repositoryAsync = repositoryAsync;
            _timbox = timbox;
        }

        public IRepositoryAsync<Factura> RepositoryAsync => _repositoryAsync;

        public async Task<Response<EstatusCancelacionDto>> Handle(CancelarFacturaCommand request, CancellationToken cancellationToken)
        {
            return await _timbox.cancelar(request.Id);

        }
    }
}
