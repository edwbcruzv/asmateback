using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.Incidencias
{
    public class IncidenciaDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int EmpleadoId { get; set; }
        public string EmpleadoNombre { get; set; }
        public string Tipo { get; set; }
        public string TipoId { get; set; }
        public string? Motivo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Observciones { get; set; }
        public string Estatus { get; set; }
        public string EstatusId { get; set; }
        public string? ArchivoSrc { get; set; }
        public string? Justificacion { get; set; }
        public int? Dias { get; set; }
    }
}
