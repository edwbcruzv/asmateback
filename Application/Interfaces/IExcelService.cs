using Application.DTOs.Administracion;
using Application.DTOs.NIF;
using Application.DTOs.ReembolsosOperativos;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExcelService
    {
        public Task<Response<SourceFileDto>> ExcelFacturas(int CompanyId); 
        public Task<Response<SourceFileDto>> ExcelComplementoPago(int CompanyId); 
        public Task<Response<SourceFileDto>> CreateExcelAsistenciaPorPeriodo(int Id);
        public Task<Response<bool>> ReadExcelAsistenciaPorPeriodo(int Id, IFormFile file);
        public Task<Response<string>> CreateExcelCalculoNif();
        public Task<List<NifResultadoDTO>> LeerExcelNif(IFormFile file);
        public Task<String> EscribirResultadoNif(List<List<NifResultadoDTO>> lista);
        public Task<String> ArchivoExcelMovimientosReembolso(List<MovimientoReembolsoDTO> listaMovimientos);
        public Task<Response<SourceFileDto>> CreateExcelReporteDeNominas(int periodoId);
        public Task<double> CalcularSalarioBaseCotizacion(Employee employee, Nomina nomina);
    }
}
