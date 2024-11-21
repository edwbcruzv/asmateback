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

namespace Application.Feautres.Administracion.Periodos.Commands.CargaExcelAsistenciasPorPeriodosCommand
{
    public class CargaExcelAsistenciasPorPeriodosCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public IFormFile file { get; set; }

    }
    public class Handler : IRequestHandler<CargaExcelAsistenciasPorPeriodosCommand, Response<bool>>
    {

        private readonly IExcelService _excelService;
        private readonly IRepositoryAsync<Periodo> _repositoryAsyncPeriodo;

        public Handler(
            IExcelService excelService, IRepositoryAsync<Periodo> repositoryAsyncPeriodo)
        {
            _excelService = excelService;
            _repositoryAsyncPeriodo = repositoryAsyncPeriodo;
        }

        public async Task<Response<bool>> Handle(CargaExcelAsistenciasPorPeriodosCommand request, CancellationToken cancellationToken)
        {

            var response = await _excelService.ReadExcelAsistenciaPorPeriodo(request.Id, request.file);
            if (response != null)
            {
                Periodo periodo = await _repositoryAsyncPeriodo.GetByIdAsync(request.Id);
                periodo.Asistencias = true;
                await _repositoryAsyncPeriodo.UpdateAsync(periodo);
            }
            return response;

        }


    }
}
