using Microsoft.AspNetCore.Http;
using Application.Wrappers;

namespace Application.Interfaces
{
    public interface IAhorroWiseService
    {

        public string SaveConstanciaPDF(IFormFile file, int id);

        public string SavePagoPDF(IFormFile file, int id);
        public Task<double> CalcularTotalAhorroWise(int employeeId, int periodo);
    }
}
