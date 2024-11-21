using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Nif
    {
        public int Id { get; set; }
        public string? NombreEjercicio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get;set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<NifResultado> Nifresultados { get; set; }
    }
}
