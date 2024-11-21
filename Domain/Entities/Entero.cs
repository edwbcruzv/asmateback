using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Entero : AuditableBaseEntity
    {
        public double Instruccion { get; set; }
        public Employee Employee { get; set; }
        public DateTime Fecha { get; set; }
        public TipoEntero Tipo { get; set; }
        public int Periodo { get; set; }
        public float Capital { get; set; }
        public float Monto { get; set; }
        public float Interes { get; set; }
        public float FondoGarantia { get; set; }
        public float FondoGarantiaInteres { get; set; }
        public float ComisionCobro { get; set; }
        public float ComisionCobroInteres { get; set; }
        public float Ahorro { get; set; }
        public float AhorroInteres { get; set; }
        public string Descripcion { get; set; }
        public User User { get; set; }
        public int Clave { get; set; }
        public EstatusEntero Estatus { get; set; }

}

    public enum EstatusEntero
    {
        Autorizado,
        NoAutorizado,
    }


    public enum TipoEntero
    {

    }

    
}
