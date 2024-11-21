using Application.DTOs.Administracion;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.Design;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Periodos.Commands.CreatePeriodosCommand
{
    public class CreatePeriodoCommand : IRequest<Response<bool>>
    {
        public int Anio { get; set; }
        public int CompanyId { get; set; }
        public int TipoPeriocidadId { get; set; }


    }
    public class Handler : IRequestHandler<CreatePeriodoCommand, Response<bool>>
    {

        private readonly IPeriodosService _periodosService;

        public Handler(IPeriodosService periodosService)
        {
            _periodosService = periodosService;
        }

        public async Task<Response<bool>> Handle(CreatePeriodoCommand request, CancellationToken cancellationToken)
        {

            var response = await _periodosService.generaPeriodos(request.Anio, request.CompanyId, request.TipoPeriocidadId);

            return response;           

        }


    }
}
