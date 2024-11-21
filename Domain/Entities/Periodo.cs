using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Periodo : AuditableBaseEntity
    {
        public int CompanyId { get; set; }
        public int Etapa { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public int Estatus { get; set; }
        public int Tipo { get; set; }
        /*
         * 1: Normal
         * 2: Extraordinario
         */
        public bool Asistencias { get; set; }
        public int TipoPeriocidadPagoId { get; set; }

        public virtual Company Company { get; set; }
        public virtual TipoPeriocidadPago TipoPeriocidadPago { get; set; }

        public virtual ICollection<Nomina> Nominas { get; set; }
    }
}
