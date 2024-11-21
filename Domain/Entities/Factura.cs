using Domain.Common;

namespace Domain.Entities
{
    public partial class Factura : AuditableBaseEntity
    {
        public int CompanyId { get; set; }
        public string? LogoSrcCompany { get; set; }
        public int ClientId { get; set; }
        public string ReceptorRfc { get; set; } //Company
        public string ReceptorRazonSocial { get; set; } //Company
        public string LugarExpedicion { get; set; } //Company
        public int UsoCfdiId { get; set; }
        public int FormaPagoId { get; set; }
        public DateTime? FechaTimbrado { get; set; }
        public string? Uuid { get; set; }
        public string? SelloCfdi { get; set; }
        public string? SelloSat { get; set; }
        public string? CadenaOriginal { get; set; }
        /*
         Estatus value:
            1: Creado
            2: Timbrado
            3: Cancelado
         */
        public short? Estatus { get; set; } 
        public int TipoMonedaId { get; set; }
        public int EmisorRegimenFiscalId { get; set; } //company
        public string? MotivoCancelacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int MetodoPagoId { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? PagoSrcPdf { get; set; }
        public int TipoComprobanteId { get; set; }
        public string EmisorRfc { get; set; } //Cliente
        public string EmisorRazonSocial { get; set; } //Cliente
        public string ReceptorDomicilioFiscal { get; set; } //Cliente
        public int ReceptorRegimenFiscalId { get; set; } //Cliente
        public int? Folio { get; set; }
        public string? FileXmlTimbrado { get; set; }
        public string? NoCertificadoSat { get; set; }


        public virtual Company Company { get; set; }
        public virtual Client Client { get; set; }
        public virtual FormaPago FormaPago { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual TipoComprobante TipoComprobante { get; set; }
        public virtual TipoMoneda TipoMoneda { get; set; }
        public virtual UsoCfdi UsoCfdi { get; set; }
        public virtual RegimenFiscal RegimenFiscal { get; set; }

        public virtual ICollection<FacturaMovimiento> FacturaMovimientos { get; set; }
        public virtual ICollection<ComplementoPagoFactura> ComplementoPagoFacturas { get; set; }
    }
}
