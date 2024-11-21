using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.Ahorros
{
    public class AhorroWiseDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int PeriodoInicial { get; set; }
        public int PeriodoFinal { get; set; }
        public EstatusOperacion Estatus { get; set; }

        public float Rendimiento { get; set; }
        public string SrcFileConstancia { get; set; }
        public string SrcFilePago { get; set; }
    }
}
