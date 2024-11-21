using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrestamoService
    {
        public void SetFechaAndEstatus(Prestamo prestamo, EstatusOperacion estatus);
        public string SaveAcuseFirmadoPDF(IFormFile file, int id);
        public string SavePagarePDF(IFormFile file, int id);
        public string SaveConstanciaRetiroPDF(IFormFile file, int id);
        public string SaveConstanciaTransferenciaPDF(IFormFile file, int id);
        public Task<bool> EnviarCorreosTransferencia(int prestamo_id);
        public Task<bool> EnviarCorreoEstatus(Prestamo prestamo);
        public Task<string> AcusePDF(int prestamo_id);
        public Task<string> PagarePDF(int prestamo_id);
        public Task<string> EstadoCuentaPDF(int prestamo_id);
    }
}
