using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Catalogos
{
    public class CodigoPostaleDto
    {
        public int Id { get; set; }
        public string CodigoPostal { get; set; }
        public string Asentamiento { get; set; }
        public string TipoAsentamiento { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
    }
}
