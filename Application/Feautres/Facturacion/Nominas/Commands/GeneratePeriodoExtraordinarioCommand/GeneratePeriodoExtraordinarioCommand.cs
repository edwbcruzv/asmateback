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


namespace Application.Feautres.Facturacion.Nominas.Commands.GeneratePeriodoExtraordinarioCommand
{
    public class GeneratePeriodoExtraordinarioCommand : IRequest<Response<int>>
    {
        public int CompanyId { get; set; }
        public int PeriodoId { get; set; }
        public int Año { get; set; }
    }

    public class Handler : IRequestHandler<GeneratePeriodoExtraordinarioCommand, Response<int>>
    {
        public IRepositoryAsync<Periodo> _repositoryAsync;
        public IMapper _mapper;

        public Handler(IRepositoryAsync<Periodo> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(GeneratePeriodoExtraordinarioCommand request, CancellationToken cancellationToken)
        {
            Periodo periodo = await _repositoryAsync.GetByIdAsync(request.PeriodoId);
            var nuevoPeriodo = _mapper.Map<Periodo>(request);
            nuevoPeriodo.Etapa = periodo.Etapa;
            nuevoPeriodo.Tipo = 2;
            nuevoPeriodo.Estatus = 1;
            nuevoPeriodo.Asistencias = false;
            // nuevoPeriodo.Desde = new DateTime((periodo.Etapa / 100), 01, 01);
            // nuevoPeriodo.Hasta = new DateTime((periodo.Etapa / 100), 12, 31);
            nuevoPeriodo.Desde = periodo.Desde;
            nuevoPeriodo.Hasta = periodo.Hasta;
            // nuevoPeriodo.TipoPeriocidadPagoId = 21;
            nuevoPeriodo.TipoPeriocidadPagoId = periodo.TipoPeriocidadPagoId;

            var data = await _repositoryAsync.AddAsync(nuevoPeriodo);


            return new Response<int>(data.Id);
        }
    }
}