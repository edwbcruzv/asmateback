using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ComplementoPagoFactura : AuditableBaseEntity
    {
        public int ComplementoPagoId { get; set; }
        public int FacturaId { get; set; }
        public int Folio { get; set; }
        public double Monto { get; set; }
        public bool? iva { get; set; }


        public virtual ComplementoPago ComplementoPago { get; set; }
        public virtual Factura Factura { get; set; }

    }
}
