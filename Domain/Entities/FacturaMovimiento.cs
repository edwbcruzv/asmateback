using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class FacturaMovimiento : AuditableBaseEntity
    {
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

        public virtual CveProducto CveProducto { get; set; }
        public virtual ObjetoImpuesto ObjetoImpuesto { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
        public virtual Factura Factura { get; set; }
    }
}
