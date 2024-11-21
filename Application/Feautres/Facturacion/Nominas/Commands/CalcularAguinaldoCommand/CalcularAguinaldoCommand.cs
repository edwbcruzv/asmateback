using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading;


namespace Application.Feautres.Facturacion.Nominas.Commands.CalcularAguinaldoCommand
{
    public class CalcularAguinaldoCommand : IRequest<Response<bool>>
    {
        public int PeriodoId { get; set; }
    }

    public class Handler : IRequestHandler<CalcularAguinaldoCommand, Response<bool>>
    {
        
        public INominaService _nominaService;

        public Handler(INominaService nominaService)
        {
            _nominaService = nominaService;
        }

        public Task<Response<bool>> Handle(CalcularAguinaldoCommand request, CancellationToken cancellationToken)
        {
            return _nominaService.generateAguinaldoByPeriodo(request.PeriodoId);
        }
    }
}