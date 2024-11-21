using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TipoMoneda : AuditableBaseEntity
    {

        public string Pais { get; set; }
        public string Modena { get; set; }
        public string CodigoIso { get; set; }
        public string? Culture { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPagos { get; set; }
        public virtual ICollection<MovimientoReembolso> MovimientosReembolso { get; set; }
    }
}
