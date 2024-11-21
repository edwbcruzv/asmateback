using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoIncidencia : AuditableBaseEntity
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Incidencia> Incidencias { get; set; }

    }
}
