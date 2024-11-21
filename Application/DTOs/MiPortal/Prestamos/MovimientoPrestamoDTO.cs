using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.Prestamos
{
    public class MovimientoPrestamoDTO
    {
        public int PrestamoId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }
        public int Periodo { get; set; }
        public float Monto { get; set; }
        public float Rendimiento { get; set; }
        public EstadoTransaccion EstadoTransaccion { get; set; }

        public float Capital { get; set; }
        public float FondoGarantia { get; set; }
        public float SaldoActual { get; set; }
        public float Interes { get; set; }
        public float Moratorio { get; set; }
    }
}
