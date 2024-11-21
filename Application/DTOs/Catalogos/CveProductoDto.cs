using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Catalogos
{
    public class CveProductoDto
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }

    }
}
