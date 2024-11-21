using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MovimientoAhorroVoluntario
    {

        public int AhorroVoluntarioId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }
        public int Periodo { get; set; }
        public float Monto { get; set; }
        public float Rendimiento { get; set; }
        public EstadoTransaccion EstadoTransaccion { get; set; }

        public float Interes {  get; set; }

        // Propiedades de navegación
        public virtual Employee Employee { get; set; }
        public virtual Company Company { get; set; }

        // Relación N:1 con Operacion
        public virtual AhorroVoluntario AhorroVoluntario { get; set; }
    }
}
