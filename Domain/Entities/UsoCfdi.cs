using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class UsoCfdi : AuditableBaseEntity
    {

        public string UsoDeCfdi { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Nomina> Nominas { get; set; }
    }
}
