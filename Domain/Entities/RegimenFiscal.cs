using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class RegimenFiscal : AuditableBaseEntity
    {
        public string RegimenFiscalCve { get; set; }
        public string RegimenFiscalDesc { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPagos { get; set; }
        public virtual ICollection<MovimientoReembolso> MovimientosReembolso { get; set; }
        public virtual ICollection<Nomina> Nominas { get; set; }
    }
}
