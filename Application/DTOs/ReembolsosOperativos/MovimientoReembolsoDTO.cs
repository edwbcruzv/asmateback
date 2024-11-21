using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ReembolsosOperativos
{
    public class MovimientoReembolsoDTO
    {
        public int Id { get; set; }

        public string Uuid { get; set; }
        public string Concepto { get; set; }

        public string EmisorRFC { get; set; }
        public string EmisorNombre { get; set; }

        public string ReceptorRFC { get; set; }
        public string ReceptorNombre { get; set; }

        public string LugarExpedicion { get; set; }
        public DateTime FechaTimbrado { get; set; }

        public double IVATrasladados { get; set; } // TotalImpuestosTrasladados
        public double IVARetenidos { get; set; } // Impuesto="002" de cfdi:Retenciones (si y solo si)
        public double ISR { get; set; } //Impuesto="001" de cfdi:Retenciones (si y solo si)
        public double IEPS { get; set; } // (Subtotal - (IVA trasladado/0.16)) gasolina y despensa
        public double ISH { get; set; } // hoteles

        public string TipoImpuesto { get; set; }
        public string? TipoMoneda { get; set; }
        public float? TipoCambio { get; set; }
        public string RegimenFiscal { get; set; } // del emisor
        public string Reembolso { get; set; }
        public string TipoComprobante { get; set; }
        public string TipoReembolso { get; set; }
        public string FormaPago { get; set; }
        public string MetodoPago { get; set; }

        public double Subtotal { get; set; }
        public double Total { get; set; }

        public DateTime FechaMovimiento { get; set; }
        public int? AnoyMes { get; set; }
        public string? LineaCaptura { get; set; }
        public DateTime? Created { get; set; }
        public string PDFSrcFile { get; set; }
        public string XMLSrcFile { get; set; }
    }
}
