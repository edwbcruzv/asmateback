using System;
using System.Collections.Generic;

namespace Application.DTOs.Catalogos
{
    public class UnidadMedidaDto
    {
        public int Id { get; set; }
        public string UnidadDeMedida { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        //public virtual ICollection<FacturaMovimiento> FacturaMovimientos { get; set; }
    }
}
