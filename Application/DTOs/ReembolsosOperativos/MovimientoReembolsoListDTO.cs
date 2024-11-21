using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ReembolsosOperativos
{
    public class MovimientoReembolsoListDTO
    {
        public int Id { get; set; }

        public string EmisorRFC { get; set; }
        public string EmisorNombre { get; set; }
        public string Concepto { get; set; }

        public string TipoMoneda { get; set; }
        public string TipoReembolso { get; set; }

        public double Total { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string PDFSrcFile { get; set; }
        public string XMLSrcFile { get; set; }
    }
}
