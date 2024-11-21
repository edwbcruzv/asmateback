using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoEstatusReembolso : AuditableBaseEntity
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Reembolso> Reembolsos { get; set; }
    }
}
