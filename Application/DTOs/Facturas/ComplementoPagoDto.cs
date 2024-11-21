using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Facturas
{
    public class ComplementoPagoDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CompanyId { get; set; }
        public string? LogoSrcCompany { get; set; }
        public string LugarExpedicion { get; set; }
        public string EmisorRfc { get; set; } //company
        public string EmisorRazonSocial { get; set; } //company
        public int EmisorRegimenFiscalId { get; set; } //company
        public string ReceptorRfc { get; set; } //Company
        public string ReceptorRazonSocial { get; set; } //Company
        public string ReceptorDomicilioFiscal { get; set; } //Cliente
        public int ReceptorRegimenFiscalId { get; set; } //Cliente
        public DateTime FechaPago { get; set; }
        public string? PagoSrcPdf { get; set; }
        public int Folio { get; set; }
        public int Estatus { get; set; }
        public int FormaPagoId { get; set; }
        public int TipoMonedaId { get; set; }
        public string? FileXmlTimbrado { get; set; }
        public double MontoTotal { get; set; }
        public DateTime? FechaTimbrado { get; set; }
        public DateTime? Created { get; set; }

    }
}
