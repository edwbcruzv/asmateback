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

namespace Application.Feautres.Facturacion.Facturas.Commands.ExcelFacturasCommand
{
    public class ExcelFacturasCommand : IRequest<Response<SourceFileDto>>
    {
        public int CompanyId { get; set; }

    }
    public class Handler : IRequestHandler<ExcelFacturasCommand, Response<SourceFileDto>>
    {
        
        private readonly IExcelService _excelFacturacion;



        public Handler(IExcelService excelFacturacion)
        {

            _excelFacturacion = excelFacturacion;
        }



        public async Task<Response<SourceFileDto>> Handle(ExcelFacturasCommand request, CancellationToken cancellationToken)
        {
            return await _excelFacturacion.ExcelFacturas(request.CompanyId);

        }
    }
}
