using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRetiroAhorroVoluntarioService
    {
        public string SaveSolicitudFirmada(IFormFile file_solicitud_firmada, int retiro_ahorro_id, int ahorro_id);
        public string SaveConstanciaTransferenciaPDF(IFormFile fileContanciaTransferencia, int retiro_ahorro_id, int ahorro_id);
        public Task<bool> AddMovimientoRetiroAhorroVoluntario(RetiroAhorroVoluntario retiro_ahorro_voluntario, AhorroVoluntario ahorro_voluntario);
        public Task<bool> EnviarCorreosTransferencia(RetiroAhorroVoluntario retiro_ahorro_voluntario, AhorroVoluntario ahorro_voluntario);
        public Task<bool> EnviarCorreoEstatus(RetiroAhorroVoluntario retiro_ahorro_voluntario, AhorroVoluntario ahorro_voluntario);
        public Task<string> SolicitudRetiroPDF(int retiro_id, int ahorro_id);
        public Task<string> ConstanciaTransferenciaPDF(int retiro_id, int ahorro_id);
        string? SaveConstanciaPagoPDF(IFormFile srcDocContanciaPago, int id, int ahorroVoluntarioId);
    }
}
