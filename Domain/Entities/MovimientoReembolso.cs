using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MovimientoReembolso : AuditableBaseEntity
    {
        public string? Uuid { get; set; }
        public string? Concepto { get; set; }

        public string? EmisorRFC { get; set; }
        public string? EmisorNombre { get; set; }

        public string? ReceptorRFC { get; set; }
        public string? ReceptorNombre { get; set; }

        public string? LugarExpedicion { get; set; }
        public DateTime FechaTimbrado { get; set; }

        public double? IVATrasladados { get; set; } // TotalImpuestosTrasladados
        public double? IVARetenidos { get; set; } // Impuesto="002" de cfdi:Retenciones (si y solo si)
        public double? ISR { get; set; } //Impuesto="001" de cfdi:Retenciones (si y solo si)
        public double? IEPS { get; set; } // (Subtotal - (IVA trasladado/0.16)) gasolina y despensa
        public double? ISH { get; set; } // hoteles

        public string? PDFSrcFile { get; set; }
        public string? XMLSrcFile { get; set; }

        public int? TipoImpuestoId { get; set; }
        public int? TipoMonedaId { get; set; }
        public double? TipoCambio { get; set; }
        public int? RegimenFiscalId { get; set; } // del emisor
        public int ReembolsoId { get; set; }
        public int? TipoComprobanteId { get; set; }
        public int? TipoReembolsoId { get; set; }
        public int? FormaPagoId { get; set; }
        public int? MetodoPagoId { get; set; }

        public double Subtotal { get; set; }
        public double Total { get; set; }

        public DateTime? FechaMovimiento { get; set; }
        public int? AnoyMes { get; set; }
        public string? LineaCaptura { get; set; }

        public virtual TipoImpuesto? TipoImpuesto { get; set; }
        public virtual TipoMoneda? TipoMoneda { get; set; }
        public virtual RegimenFiscal? RegimenFiscal { get; set; }
        public virtual Reembolso Reembolso { get; set; }
        public virtual TipoComprobante? TipoComprobante { get; set; }
        public virtual TipoReembolso? TipoReembolso { get; set;}
        public virtual FormaPago? FormaPago { get; set; }
        public virtual MetodoPago? MetodoPago { get; set; }

    }
}
