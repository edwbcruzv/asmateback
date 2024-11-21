using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.Ahorros
{
    public class RetiroAhorroVoluntarioDTO
    {
        public int Id { get; set; }
        public int AhorroVoluntarioId { get; set; }
        public EstatusOperacion Estatus { get; set; }
        public double Cantidad { get; set; }
        public double Porcentaje { get; set; }
        public bool SeguirAhorrando { get; set; }
        public string SrcDocSolicitudFirmado { get; set; }
        public DateTime FechaTransferencia { get; set; }
        public string SrcDocContanciaTransferencia { get; set; }
    }
}
