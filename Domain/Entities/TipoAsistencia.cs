using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class TipoAsistencia : AuditableBaseEntity
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public bool? SePagaPtu { get; set; }
        public bool? SePagaAguinaldo { get; set; }
        public bool? SePagaNomina { get; set; }
        public bool? SePagaIncapacidad { get; set; }

        public virtual ICollection<RegistroAsistencia> RegistroAsistencias { get; set; }

    }
}
