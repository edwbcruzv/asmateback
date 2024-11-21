using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Facturas
{
    public class ComplementoPagoFacturaDto
    {
        public int Id { get; set; }
        public int ComplementoPagoId { get; set; }
        public int FacturaId { get; set; }
        public string ReceptorRfc { get; set; } //Company
        public string ReceptorRazonSocial { get; set; } //Company
        public string? FileXmlTimbrado { get; set; }
        public string? FilePdf { get; set; }
        public int Folio { get; set; }
        public double Monto { get; set; }
    }
}
