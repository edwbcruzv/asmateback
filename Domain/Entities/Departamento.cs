using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Departamento : AuditableBaseEntity
    {
        public int CompanyId { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Puesto> Puestos { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<SistemaDepartamento> SistemaDepartamento { get; set; }

    }
}
