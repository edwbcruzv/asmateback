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

namespace Application.Feautres.Facturacion.Facturas.Commands.TimbrarFacturaCommand
{
    public class TimbrarFacturaCommand : IRequest<Response<bool>>
    {

        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<TimbrarFacturaCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsync;
        private readonly ITimboxService _timboxPruebas;



        public Handler(IRepositoryAsync<Factura> repositoryAsync, ITimboxService timboxPruebas)
        {
            _repositoryAsync = repositoryAsync;
            _timboxPruebas = timboxPruebas;
        }

        public IRepositoryAsync<Factura> RepositoryAsync => _repositoryAsync;

        public async Task<Response<bool>> Handle(TimbrarFacturaCommand request, CancellationToken cancellationToken)
        {
            return await _timboxPruebas.timbrar(request.Id);

        }
    }
}
