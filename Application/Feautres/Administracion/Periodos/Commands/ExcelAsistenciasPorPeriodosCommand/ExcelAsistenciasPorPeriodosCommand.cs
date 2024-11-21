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

namespace Application.Feautres.Administracion.Periodos.Commands.ExcelAsistenciasPorPeriodosCommand
{
    public class ExcelAsistenciasPorPeriodosCommand : IRequest<Response<SourceFileDto>>
    {
        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<ExcelAsistenciasPorPeriodosCommand, Response<SourceFileDto>>
    {

        private readonly IExcelService _excelService;

        public Handler(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public async Task<Response<SourceFileDto>> Handle(ExcelAsistenciasPorPeriodosCommand request, CancellationToken cancellationToken)
        {

            var response = await _excelService.CreateExcelAsistenciaPorPeriodo(request.Id);

            return response;

        }


    }
}
