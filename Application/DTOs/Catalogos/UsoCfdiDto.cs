using System;
using System.Collections.Generic;

namespace Application.DTOs.Catalogos
{
    public class UsoCfdiDto
    {

        public int Id { get; set; }
        public string UsoDeCfdi { get; set; }
        public string Descripcion { get; set; }

        // public virtual ICollection<Factura> Facturas { get; set; }
    }
}
