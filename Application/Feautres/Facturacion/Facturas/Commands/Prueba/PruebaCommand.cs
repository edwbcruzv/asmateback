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

namespace Application.Feautres.Facturacion.Facturas.Commands.Prueba
{
    public class PruebaCommand : IRequest<Response<string>>
    {

    }
    public class Handler : IRequestHandler<PruebaCommand, Response<string>>
    {

        private readonly ITimboxService _timboxPruebas;



        public Handler(ITimboxService timboxPruebas)
        {
            _timboxPruebas = timboxPruebas;
        }


        public async Task<Response<string>> Handle(PruebaCommand request, CancellationToken cancellationToken)
        {
            return await _timboxPruebas.prueba();

        }
    }
}
