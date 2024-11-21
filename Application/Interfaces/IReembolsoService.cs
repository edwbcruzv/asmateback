using Application.DTOs.Administracion;
using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReembolsoService
    {
        public Task<Response<string>> CrearExcelMovimientoReembolso(int reembolsoId);

        public Task<Response<string>> DescargaMasivaReembolsos(int[]ids);

        public Task<double> CalcularMontoTotalReembolso(int reembolsoId);

        public Task<Dictionary<string, double>> ObtenerTotalesReembolso(int reembolsoId);
    }
}
