using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class UnidadMedida : AuditableBaseEntity
    {

        public string UnidadDeMedida { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public virtual ICollection<FacturaMovimiento> FacturaMovimientos { get; set; }
    }
}
