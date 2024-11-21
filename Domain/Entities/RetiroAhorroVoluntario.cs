using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RetiroAhorroVoluntario : AuditableBaseEntity
    {

        public int AhorroVoluntarioId { get; set; }
        public EstatusRetiro Estatus { get; set; }
        public double Cantidad { get; set; } 
        public double Porcentaje { get; set; }
        public bool SeguirAhorrando { get; set; }
        public string? SrcDocSolicitudFirmado { get; set; }
        public DateTime? FechaTransferencia { get; set; }
        public string? SrcDocContanciaTransferencia { get; set; }
        public string? SrcDocContanciaPago { get; set; }

        public DateTime FechaEstatusPendiente { get; set; }
        public DateTime? FechaEstatusAutorizado { get; set; }
        public DateTime? FechaEstatusRechazado { get; set; }

        public virtual AhorroVoluntario AhorroVoluntario { get; set; }
    }

    public enum EstatusRetiro
    {
        [Description("Pendiente")]
        Pendiente,
        [Description("Autorizado")]
        Autorizado,
        [Description("Rechazado")]
        Rechazado,
    }
}
