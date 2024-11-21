using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TipoComprobante : AuditableBaseEntity
    {
        public string TipoDeComprobante { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<MovimientoReembolso> MovimientosReembolso { get; set; }
    }
}
