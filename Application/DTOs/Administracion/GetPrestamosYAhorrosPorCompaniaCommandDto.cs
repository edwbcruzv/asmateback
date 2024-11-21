using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Administracion
{
    public class GetPrestamosYAhorrosPorCompaniaCommandDto
    {
        public string Tipo { get; set; }
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public double Monto { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string AcuseSrc { get; set; }
        public string PagareSrc { get; set; }
        public int EmployeeId { get; set; }
        public int AhorroId { get; set; }
    }
}
