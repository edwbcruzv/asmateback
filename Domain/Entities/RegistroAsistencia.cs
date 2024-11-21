using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RegistroAsistencia : AuditableBaseEntity
    {
        public int EmployeeId { get; set; }
        public DateTime Dia { get; set; }
        public int TipoAsistenciaId { get; set; }
        public int Comentarios { get; set; }

        public Employee Employee { get; set; }
        public TipoAsistencia TipoAsistencia { get; set; }

    }
}
