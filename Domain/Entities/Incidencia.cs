using Domain.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Incidencia : AuditableBaseEntity

    {
        public virtual Company Company { get; set; }
        public virtual Employee Employee { get; set; }
        public int CompanyId { get; set; }
        public int EmpleadoId { get; set; }
        public int TipoId { get; set; }
        public string? Motivo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Observciones { get;set; }    
        public int EstatusId { get; set; }
        public string? ArchivoSrc { get; set; }
        public string? Justificacion { get; set; }
        public int? Dias {  get; set; }
        public virtual TipoIncidencia TipoIncidencia { get; set; }
        public virtual TipoEstatusIncidencia TipoEstatusIncidencia { get; set; }

    }
}
