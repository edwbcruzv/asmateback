using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ReembolsosOperativos
{
    public class ReembolsoDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string? Clabe { get; set; }
        /*
         Posibles valores de Estatus:
            1 - Abierto
            2 - Cerrado
            3 - Pagado
         */ 
        public string Estatus { get; set; }
        //public string? SrcPdfPagoComprobante { get; set; }
        public double Monto { get; set; }
        public DateTime? FechaPago { get; set; }
        public int CompanyId { get; set; }
        public string CompanyRFC { get; set; }
        public int? UsuarioIdPago { get; set; }
        public string? UsuarioName { get; set; }
        public int? EstatusId { get; set; }
        public DateTime? Created { get; set; }
        public string? SrcPdfPagoComprobante { get; set; }
    }
}
