using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosWise;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Shared.Services
{
    public class AhorroWiseService : IAhorroWiseService
    {
        private readonly IRepositoryAsync<AhorroWise> _repositoryAsyncAhorroWise;
        private readonly IRepositoryAsync<MovimientoAhorroWise> _repositoryAsyncMovimientoAhorroWise;
        private readonly IFilesManagerService _filesManagerService;

        public AhorroWiseService(IRepositoryAsync<AhorroWise> repositoryAsyncAhorroWise,
            IFilesManagerService filesManagerService,
            IRepositoryAsync<MovimientoAhorroWise> repositoryAsyncMovimientoAhorroWise)
        {
            _repositoryAsyncAhorroWise = repositoryAsyncAhorroWise;
            _filesManagerService = filesManagerService;
            _repositoryAsyncMovimientoAhorroWise = repositoryAsyncMovimientoAhorroWise;
        }

        public string SaveConstanciaPDF(IFormFile file, int id)
        {
            return _filesManagerService.saveFileInTo(file, id, "StaticFiles\\Mate", "FormatosPDF\\AhorrosWise\\Constancias", ".pdf");
        }

        public string SavePagoPDF(IFormFile file, int id)
        {
            return _filesManagerService.saveFileInTo(file, id, "StaticFiles\\Mate", "FormatosPDF\\AhorrosWise\\Pagos", ".pdf");
        }

        public async Task<double> CalcularTotalAhorroWise(int employeeId, int periodo)
        {
            var movimientosAhorroWise = await _repositoryAsyncMovimientoAhorroWise.ListAsync(new MovimientoAhorroWiseByEmployeeIdSpecification(employeeId));
            var totalAhorroWise = 0.0;
            foreach (var movimiento in movimientosAhorroWise)
            {
                if(movimiento.Periodo <= periodo)
                {
                    totalAhorroWise += movimiento.Monto;
                }
            }
            return totalAhorroWise;
        }

    }
}
