using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class CveProducto : AuditableBaseEntity
    {
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }
        public virtual ICollection<FacturaMovimiento> FacturaMovimientos { get; set; }

    }
}
