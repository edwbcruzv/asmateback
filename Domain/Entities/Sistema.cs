using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sistema : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string? Color { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<SistemaDepartamento> SistemaDepartamento { get; set; }

    }
}
