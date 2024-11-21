using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Facturas
{
    public class FacturaPDDDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ClientId { get; set; }
        public string ReceptorRfc { get; set; }
        public string ReceptorRazonSocial { get; set; }
        public string LugarExpedicion { get; set; }
        public DateTime? FechaTimbrado { get; set; }
        public DateTime? FechaPago { get; set; }
        public string EmisorRfc { get; set; }
        public string EmisorRazonSocial { get; set; }
        public DateTime? Created { get; set; }
        public double MontoTotal { get; set; }
        public double SaldoIsoluto { get; set; }

    }
}
