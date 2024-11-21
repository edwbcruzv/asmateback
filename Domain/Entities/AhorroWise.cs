using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AhorroWise : AuditableBaseEntity
    {
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int PeriodoInicial { get; set; }
        public int? PeriodoFinal { get; set; }
        public EstatusOperacion Estatus { get; set; }

        public float? Rendimiento { get; set; }
        public string? SrcFileConstancia { get; set; }
        public string? SrcFilePago { get; set; }

        // Propiedades de navegación
        public virtual Employee Employee { get; set; }
        public virtual Company Company { get; set; }

        // Relación 1:N con Movimiento
        public virtual ICollection<MovimientoAhorroWise> Movimientos { get; set; }
    }
}
