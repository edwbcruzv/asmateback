using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ImssDescuento : AuditableBaseEntity
    {
        public string Descripcion { get; set; }
        public double Trabajador { get; set; }
        public double Patron { get; set; }
        public double Inicio { get; set; }
        public double Fin { get; set; }
        public short Exc { get; set; }
    }
}
