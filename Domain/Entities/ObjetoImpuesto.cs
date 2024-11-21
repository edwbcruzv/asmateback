using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ObjetoImpuesto : AuditableBaseEntity
    {

        public string ObjetosImpuesto{ get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<FacturaMovimiento> FacturaMovimientos { get; set; }
    }
}
