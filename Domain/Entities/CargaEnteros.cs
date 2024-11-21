using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CargaEnteros :AuditableBaseEntity
    {
        public double Folio { get; set; }
        public Company Company { get; set; }
        public DateTime Fecha { get; set; }
        public int Periodo { get; set; }
        public float Monto { get; set; }
        public int Enteros { get; set; }
        
    }

    public enum TipoCargaEnteros
    {
        Prestamo,
        AhorroVol,
        FondoAhorro
    }

    public enum EstatusCargaEnteros
    {
        Activo,
        Inactivo
    }
}
