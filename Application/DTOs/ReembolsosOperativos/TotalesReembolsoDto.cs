using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ReembolsosOperativos
{
    public class TotalesReembolsoDto
    {
        public string? Usuario { get; set; }
        public DateTime? FechaCaptutra { get; set; }
        public double Subtotal { get; set; }
        public double Descuento { get; set; }
        public double ImpuestosRetenidos { get; set; }
        public double ImpuestosTrasladados { get; set; }
        public double IEPS { get; set; }
        public double ISH { get; set; }
        public double TotalPagar { get; set; }
        public string Descripcion { get; set; }
        public string? Cuenta { get; set; }
        public string? Banco { get; set; }
    }
}
