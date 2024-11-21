using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class FormaPago : AuditableBaseEntity
    {
        public string FormaDePago { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPagos { get; set; }
        public virtual ICollection<MovimientoReembolso> MovimientosReembolso { get; set; }

    }
}
