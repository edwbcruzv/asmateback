using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.Comprobantes
{
    public class ComprobanteDTO
    {
        public int ViaticoId { get; set; }
        public int Id { get; set; }
        public string? EmisorRFC { get; set; }
        public float Total { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Concepto { get; set; }
        public TipoComprobantes TipoComprobantes { get; set; }
        public int? TipoMonedaId { get; set; }
        public string? Moneda { get; set; }
        public string? PathXML { get; set; }
        public string? PathPDF { get; set; }
    }
}
