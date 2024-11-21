using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class MetodoPago : AuditableBaseEntity
    {
        public string MetodoDePago { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Nomina> Nominas { get; set; }
        public virtual ICollection<MovimientoReembolso> MovimientosReembolso { get; set; }

    }
}
