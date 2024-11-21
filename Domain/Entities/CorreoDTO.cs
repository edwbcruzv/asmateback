using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CorreoDTO
    {
        public string correo { get; set; }
        public int IdFideicomiso { get; set; }
        public string DescripcionCorreo { get; set; }
        public string fileName { get; set; }
    }
}
