using Application.DTOs.Administracion;
using Application.DTOs.Facturas;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPdfService
    {
        public Task<Response<FacturaPdfDto>> PdfFactura(int Id); 
        public Task<Response<SourceFileDto>> PdfComplementoPago(int Id);
        public Task<Response<NominaPdfDto>> PdfNomina(int Id);
        public Task<string> PdfVacaciones(int Id, int aniosTrabajados,int diasVacaciones);
        public Task<string> PdfIncidencia(int Id);
        public Task<string> PdfEstadoDeCuentaWise(int ahorroWiseId, int periodo, int companyId);
    }
}
