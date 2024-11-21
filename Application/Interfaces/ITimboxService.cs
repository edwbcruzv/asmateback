using Application.DTOs.Facturas;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITimboxService
    {
        public Task<Response<bool>> timbrar(int Id); 
        public Task<Response<bool>> timbrarComplementoPago(int Id);
        public Task<Response<bool>> timbrarNomina(int Id);
        public Task<Response<EstatusCancelacionDto>> cancelar(int Id);
        public Task<Response<EstatusCancelacionDto>> consultarEstatus(int Id);
        public Task<Response<EstatusCancelacionDto>> cancelarComplementoPago(int Id);
        public Task<Response<EstatusCancelacionDto>> consultarEstatusComplementoPago(int Id);
        public Task<Response<EstatusCancelacionDto>> cancelarNomina(int Id);
        public Task<Response<EstatusCancelacionDto>> consultarEstatusNomina(int Id);
        public Task<Response<string>> prueba();
        public string getNoCertificado(string fileCertificado);
    }
}
