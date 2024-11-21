using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.Ahorros
{
    public class AhorroVoluntarioDTO
    {


        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeNombreCompleto { get; set; }
        public string EmployeeRFC { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int PeriodoInicial { get; set; }
        public int PeriodoFinal { get; set; }
        public EstatusOperacion Estatus { get; set; }
        public string EstatusString { get; set; }
        public string SrcCartaFirmada { get; set; }

        public float MontoTotal { get; set; }

        public float Rendimiento { get; set; }
        public float Descuento { get; set; }


        public virtual ICollection<MovimientoAhorroVoluntarioDTO> Movimientos { get; set; }
    }
}
