using Application.DTOs.Administracion;
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

namespace Application.Feautres.Facturacion.ComplementoPagos.Commands.ExcelComplementoPagoCommand
{
    public class ExcelComplementoPagoCommand : IRequest<Response<SourceFileDto>>
    {
        public int CompanyId { get; set; }
    }
    public class Handler : IRequestHandler<ExcelComplementoPagoCommand, Response<SourceFileDto>>
    {
        
        private readonly IExcelService _excelFacturacion;



        public Handler(IExcelService excelFacturacion)
        {

            _excelFacturacion = excelFacturacion;
        }



        public async Task<Response<SourceFileDto>> Handle(ExcelComplementoPagoCommand request, CancellationToken cancellationToken)
        {
            return await _excelFacturacion.ExcelComplementoPago(request.CompanyId);

        }
    }
}
