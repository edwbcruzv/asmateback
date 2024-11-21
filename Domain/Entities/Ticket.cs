using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ticket : AuditableBaseEntity
    {
        public int TipoSolicitudTicketId { get; set; }
        public int CompanyId { get; set; }
        public int SistemaId { get; set; }
        public int DepartamentoId { get; set; }
        public int EmployeeAsignadoId { get; set; }
        public int EmployeeCreadorId { get; set; }
        public int EstadoId { get; set; }
        public string OpcionMenu { get; set; }
        public string? OpcionSubMenu { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? SrcImagen { get; set; }
        public EstatusTicket Estatus { get; set; }
        public Prioridad Prioridad { get; set; }
       

        public virtual TipoSolicitudTicket TipoSolicitudTicket { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual Company Company { get; set; }
        public virtual Employee EmployeeAsignado { get; set; }
        public virtual Employee EmployeeCreador { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Sistema Sistema { get; set; }


    }

    public enum EstatusTicket
    {
        Abierto,
        Revision,
        Cerrado
    }

    public enum Prioridad
    {
        Baja,
        Media,
        Alta
    }
}
