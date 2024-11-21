using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Administracion
{
    public class PeriodoDto 
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int Etapa { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public int Estatus { get; set; }
        public int Tipo { get; set; }
        public bool Asistencias { get; set; }
        public int TipoPeriocidadPagoId { get; set; }

    }
}
