using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Facturas
{
    public class FacturaMovimientoDto
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int Cantidad { get; set; }
        public int UnidadMedidaId { get; set; }
        public int CveProductoId { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public bool Iva { get; set; }
        public bool Iva6 { get; set; }
        public bool RetencionIva { get; set; }
        public bool RetencionIsr { get; set; }
        public int ObjetoImpuestoId { get; set; }
    }
}
