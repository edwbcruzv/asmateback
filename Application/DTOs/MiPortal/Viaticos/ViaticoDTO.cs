using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.Viaticos
{
    public class ViaticoDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Fecha { get; set; }
        public int CompanyId { get; set; }
        public string Estatus { get; set; }
        public int EstatusId { get; set; }
        public float Monto { get; set; }
        public String Descripcion { get; set; }
        public int? BancoId { get; set; }
        public string? NoCuenta { get; set; }
        public IFormFile? PDF { get; set; }
        public int? EmployeePagoId { get; set; }
        public DateTime? FechaPago { get; set; }
        public float? MontoRecibido { get; set; }
        public string? Tipo { get; set; }
        public string ? Company { get; set; }
        public float? MontoExcedente { get; set; }
        public int EstadoId { get; set; }
        public string Estado { get; set; }
    }
}
