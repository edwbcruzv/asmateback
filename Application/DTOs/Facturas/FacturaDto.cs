using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Facturas
{
    public class FacturaDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string? LogoSrcCompany { get; set; }
        public int ClientId { get; set; }
        public string ReceptorRfc { get; set; }
        public string ReceptorRazonSocial { get; set; }
        public string LugarExpedicion { get; set; }
        public int UsoCfdiId { get; set; }
        public int FormaPagoId { get; set; }
        public DateTime? FechaTimbrado { get; set; }
        public short? Estatus { get; set; }
        public int TipoMonedaId { get; set; }
        public int EmisorRegimenFiscalId { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int MetodoPagoId { get; set; }
        public DateTime? FechaPago { get; set; }
        public int TipoComprobanteId { get; set; }
        public string EmisorRfc { get; set; }
        public string EmisorRazonSocial { get; set; }
        public string ReceptorDomicilioFiscal { get; set; }
        public int ReceptorRegimenFiscalId { get; set; }
        public int? Folio { get; set; }
        public string? FileXmlTimbrado { get; set; }
        public DateTime? Created { get; set; }
        public string PagoSrcPdf { get; set; }
        public double MontoTotal { get; set; }
    }
}
