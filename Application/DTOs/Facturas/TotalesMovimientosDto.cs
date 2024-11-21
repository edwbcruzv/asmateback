using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Facturas
{
    public class TotalesMovimientosDto
    {
        public decimal total {get; set; }
        public decimal subTotal {get; set;}
        public decimal descuentoTotal {get; set;}

        public decimal iva {get; set;}
        public decimal iva6 {get; set;}
        public decimal retencionISR {get; set;}
        public decimal retencionIva {get; set;}

        public decimal trasladadosTotal {get; set;}
        public decimal retenidosTotal {get; set;}

        public decimal retencionIva6Total {get; set;}
        public decimal retencionIvaTotal {get; set;}
        public decimal retencionIsrTotal {get; set;}

        public decimal baseIva {get; set; }

        public bool tieneTraslados {get; set;}
        public bool tieneRetencionIva {get; set;}
        public bool tieneRetencionIsr {get; set;}
        public bool tieneRetencionIva6 {get; set;}
    }
}
