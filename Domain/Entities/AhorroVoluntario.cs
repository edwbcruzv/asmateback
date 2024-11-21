using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AhorroVoluntario : AuditableBaseEntity
    {
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int? PeriodoInicial { get; set; }
        public int? PeriodoFinal { get; set; }
        public EstatusOperacion Estatus { get; set; }
        public string? SrcCartaFirmada { get; set; }
        public DateTime? FechaTransferencia { get; set; }
        public string? SrcDocContanciaTransferencia { get; set; }

        public DateTime FechaEstatusPendiente { get; set; }
        public DateTime? FechaEstatusActivo { get; set; }
        public DateTime? FechaEstatusRechazado { get; set; }
        public DateTime? FechaEstatusFiniquitado { get; set; }

        public float Rendimiento { get; set; }
        public float Descuento { get; set; }

        // Propiedades de navegación
        public virtual Employee Employee { get; set; }
        public virtual Company Company { get; set; }

        // Relación 1:N con Movimiento
        public virtual ICollection<MovimientoAhorroVoluntario> Movimientos { get; set; }


        // Método para calcular la suma de la propiedad Cantidad de los movimientos
        public float SumaMontoTotalMov()
        {
            if (Movimientos == null)
                return 0;

            return Movimientos.Sum(m => m.Monto);
        }
    }
}
