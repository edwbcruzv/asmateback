using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class NominaPercepcion : AuditableBaseEntity
    {
        public int NominaId { get; set; }
        public string Tipo { get; set; }
        public string Clave { get; set; }
        public string Concepto { get; set; }
        public double ImporteGravado { get; set; }
        public double ImporteExento { get; set; }
        
        public virtual Nomina Nomina { get; set; }
    }
}
