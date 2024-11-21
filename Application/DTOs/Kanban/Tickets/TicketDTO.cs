using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Kanban.Tickets
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public int TipoSolicitudTicketId { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int SistemaId { get; set; }
        public string Sistema { get; set; }
        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
        public int EmployeeAsignadoId { get; set; }
        public string EmployeeAsignado { get; set; }
        public string EmployeeAsignadoCompl { get; set; }
        public int EmployeeCreadorId { get; set; }
        public string EmployeeCreador { get; set; }
        public string EmployeeCreadorCompl { get; set; }
        public int EstadoId { get; set; }
        public string OpcionMenu { get; set; }
        public string OpcionSubMenu { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? SrcImagen { get; set; }
        public EstatusTicket Estatus { get; set; }
        public string Created { get; set; }
        public Prioridad Prioridad { get; set; }
    }
}
