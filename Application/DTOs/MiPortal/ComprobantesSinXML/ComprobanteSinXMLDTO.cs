using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.ComprobantesSinXML
{
    public class ComprobanteSinXMLDTO
    {
        public int Id { get; set; }
        public int ViaticoId { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Concepto { get; set; }
        public string PathPDF { get; set; }

        public string Uuid { get; set; }
        public string Folio { get; set; }
        public string EmisorRFC { get; set; }
        public string EmisorNombre { get; set; }
        public string ReceptorRFC { get; set; }
        public string ReceptorNombre { get; set; }

        public int TipoMonedaId { get; set; }
        public int FormaPagoId { get; set; }

        public float Descuento { get; set; }
        public float SubTotal { get; set; }
        public float Total { get; set; }
    }
}
