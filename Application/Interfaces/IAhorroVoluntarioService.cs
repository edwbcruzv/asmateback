using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAhorroVoluntarioService
    {
        public string SaveCartaFirmada(IFormFile file_carta_firmada, int ahorro_id);
        public Task<bool> EnviarCorreoEstatus(AhorroVoluntario ahorro);
        public Task<double> GetDeduccion(int employee_id);
        public Task<string> CartaPDF(int ahorro_id);
        public Task<string> EstadoCuentaPDF(int ahorro_id, int periodo_inicial, int periodo_final);
        public Task<string> SolicitudRetiroPDF(int ahorro_id, float cantidad);
    }
}
