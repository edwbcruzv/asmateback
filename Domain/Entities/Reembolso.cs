using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reembolso : AuditableBaseEntity
    {

        public string Descripcion { get; set; }
        public string? Clabe { get; set; }
        public string? SrcPdfPagoComprobante { get; set; }
        public DateTime? FechaPago { get; set; }
        
        public int CompanyId { get; set; }
        public int EstatusId { get; set; }
        public int? UsuarioIdPago { get; set; }

        public virtual Company Company { get; set; }
        public virtual TipoEstatusReembolso TipoEstatusReembolso { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MovimientoReembolso> MovimientosReembolso { get; set; }
    }
}
