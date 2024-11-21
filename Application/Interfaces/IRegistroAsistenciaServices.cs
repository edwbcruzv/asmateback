using Application.DTOs.Administracion;
using Application.DTOs.MiPortal.Incidencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRegistroAsistenciaServices
    {
        public Task<List<AsistenciaResumenDto>> getAsistenciaByPeriodo(int PeriodoId, int EmpleadoId);
        public Task<List<AsistenciaResumenDto>> getAsistenciaByRango(DateTime Desde, DateTime Hasta, int EmpleadoId);
        public Task<DiasIncidenciaDto> getDiasIncidenciaByEmployee(int employeeId, int aniosTrabajados);
        public Task<int> calcularDiasVacaciones(DateTime FechaInicio, DateTime FechaFin);
    }
}
