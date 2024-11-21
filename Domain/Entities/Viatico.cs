using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Viatico : AuditableBaseEntity
    {
        public int EmployeeId { get; set; }
        public DateTime Fecha { get; set; }
        public int CompanyId { get; set; }
        public EstatusViatico Estatus { get; set; }
        public float Monto { get; set; }
        public String Descripcion { get; set; }
        public int? BancoId { get; set; }
        public string? NoCuenta { get; set; }
        public string? SrcPDF { get; set; }
        public int? EmployeePagoId { get; set; }
        public DateTime? FechaPago { get; set; }
        public float? MontoRecibido { get; set; }
        public int? EstadoId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee EmployeePago { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual ICollection<Comprobante> Comprobantes { get; set;}

    }

    public enum EstatusViatico
    {
        Abierto,
        Cerrado,
        Pagado
    }

}
