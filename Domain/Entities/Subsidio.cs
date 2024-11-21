using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Subsidio : AuditableBaseEntity
    {
        public int Anio { get; set; }
        /*
         * Periodo
         *  1 Quincenal
         *  2 Mensual
         */
        public int Periodo { get; set; }
        public double LimiteInferior { get; set; }
        public double LimiteSuperior { get; set; }
        public double CuotaFija { get; set; }
    }
}
