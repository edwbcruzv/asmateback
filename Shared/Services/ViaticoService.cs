using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.MiPortal.Comprobantes;
using Application.Wrappers;
using Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class ViaticoService : IViaticoService
    {
        private readonly IRepositoryAsync<Employee> _repositoryEmployee;
        private readonly ISendMailService _sendMailService;
        private readonly IRepositoryAsync<Comprobante> _repositoryAsyncComprobante;

        public ViaticoService(IRepositoryAsync<Employee> repositoryEmployee, ISendMailService sendMailService, IRepositoryAsync<Comprobante> repositoryAsyncComprobante)
        {
            _repositoryEmployee = repositoryEmployee;
            _sendMailService = sendMailService;
            _repositoryAsyncComprobante = repositoryAsyncComprobante;
        }

        public async Task<bool> EnviarCorreoViatico(Viatico viatico)
        {
            Employee employee = await _repositoryEmployee.GetByIdAsync(viatico.EmployeeId);

            string mailHTML = File.ReadAllText(Path.Combine(@"C:\StaticFiles", @"Mate\PlantillasCorreo\PlantillaEnviarViatico.html")).ToString();

            string nombre = employee.NombreCompletoOrdenado();
            string correo = employee.MailCorporativo;

            mailHTML = mailHTML.Replace("#Empleado#", nombre);

            string[] lista_correos = {
                    correo,
                    //"erika.aldama@solucionesintegrales-mex.com.mx",
                    //"arturo.pazgo@gmail.com"
            };

            try
            {
                _sendMailService.SendEmailWithAttachment("facturacion@maxal.com.mx", lista_correos, "Comprobación de Viáticos", mailHTML, null);
            }
            catch (Exception e)
            {
                throw new ApiException($"Ocurrio un error {e.Message}");
            }
            return true;
        }

        public async Task<float> CalcularMontoTotalViatico(int viaticoId)
        {
            var list = await _repositoryAsyncComprobante.ListAsync(new ComprobanteByViaticoIdSpecification(viaticoId));
            var total = 0;

            foreach (var item in list)
            {
                total += (int)((int)item.Total * item.TipoCambio);
            }

            return total;
        }

    }
}
