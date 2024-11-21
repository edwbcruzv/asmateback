using Application.DTOs.Facturas;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPeriodosService
    {
        public Task<Response<bool>> generaPeriodos(int Anio, int CompanyId, int TipoPeriocidadPagoId);
        public int DateTimeToQuincena(DateTime fecha);
        public DateTime QuincenaToDateTime(int quincena_int);
        public int GetQuincenaFinalByPlazoQuincenal(int quincena_inicial, int plazo_quincenal);
    }
}
