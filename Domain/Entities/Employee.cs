using Domain.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities
{
    public class Employee : AuditableBaseEntity
    {
        public int CompanyId { get; set; }
        public Int64 NoEmpleado { get; set; }
        public string? Rfc { get; set; }
        public string? Nss { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Curp { get; set; }
        public int? EstadoCivil { get; set; }
        public string? Mail { get; set; }
        public string? NoCuenta { get; set; }
        public string? CLABE { get; set; }
        public int? BancoId { get; set; }
        public int Estatus { get; set; }
        public int? UserId { get; set; }
        public string MailCorporativo { get; set; }
        public string TelefonoMovil { get; set; }
        public string? TelefonoFijo { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Calle { get; set; }
        public string? NoInt { get; set; }
        public string? NoExt { get; set; }
        public string? Estado { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime? FechaContrato { get; set; }
        public string? RelojChecador { get; set; }
        public string? RegistroPatronal { get; set; }
        public int? PuestoId { get; set; }
        public DateTime? FinContrato { get; set; }

        public double? SalarioMensual { get; set; }
        public double? SalarioDiario { get; set; }
        public double? SalarioDiarioIntegrado { get; set; }
        public double? SBC { get; set; }
        public int? TipoPrevicionSocial { get; set; } //Nuevo //1: Salarios Minimos //2: Porcentaje
        public double? Porcentaje { get; set; }
        public int? TipoContratoId { get; set; }
        public int? TipoJornadaId { get; set; } //Nuevo
        public int? FormaPagoId { get; set; } //Nuevo
        public int? TipoPeriocidadPagoId { get; set; } //Nuevo
        public int? TipoRegimenId { get; set; }
        public int? TipoRiesgoTrabajoId { get; set; }
        public int? TipoIncapacidadId { get; set; }
        public string? CreditoFonacot { get; set; }
        public string? CreditoInfonavit { get; set; }
        public double? DescuentoCreditoHipo { get; set; }
        public int? RegimenFiscalId { get; set; }
        /*
         * TIPO NOMINA
         * 1: TRADICIONAL
         * 2: MIXTA
         * 3: GASTO OPRATIVO
         */
        public int? TipoNomina { get; set; }
        /*
         * CuentaIndividual
         * 1: TRADICIONAL
         *      CuentaIndividual 
         *          2: Sí
         *              Activa FondoAhorroEmpleado, FondoAhorroEmpresa
         *          1: No 
         *              Quita FondoAhorroEmpleado, FondoAhorroEmpresa  
         * 2: MIXTO
         *      CuentaIndividual 
         *         2: Sí
         *              Activa FondoAhorroEmpleado, FondoAhorroEmpresa
         *         1: No 
         *              Quita FondoAhorroEmpleado, FondoAhorroEmpresa 
         *         3: Modelo Wise
         *              Quita FondoAhorroEmpleado, FondoAhorroEmpresa
         */
        public short? CuentaIndividual { get; set; }
        public double? FondoAhorroEmpleado { get; set; }
        public double? FondoAhorroEmpresa { get; set; }
        public double? PorcentajePrimaVacacional { get; set; }
        public double? AjusteIsr { get; set; }

        // Id del jefe del empleado
        public int? JefeId { get; set; }

        public string NombreCompleto()
        {
            if (!ApellidoMaterno.Equals("") && ApellidoMaterno != null)
                return $"{ApellidoPaterno.TrimEnd()} {ApellidoMaterno.TrimEnd()} {Nombre.TrimEnd()}";
            else
                return $"{ApellidoPaterno.TrimEnd()} {Nombre.TrimEnd()}";

        }

        public string NombreCompletoOrdenado()
        {
            if (!string.IsNullOrEmpty(ApellidoMaterno))
                return $"{Nombre.TrimEnd()} {ApellidoPaterno.TrimEnd()} {ApellidoMaterno.TrimEnd()}";
            else
                return $"{Nombre.TrimEnd()} {ApellidoPaterno.TrimEnd()}";
        }


        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual Puesto Puesto { get; set; }
        public virtual TipoContrato TipoContrato { get; set; }
        public virtual TipoRegimen TipoRegimen { get; set; }
        public virtual TipoRiesgoTrabajo TipoRiesgoTrabajo { get; set; }
        public virtual TipoIncapacidad TipoIncapacidad { get; set; }
        public virtual RegimenFiscal RegimenFiscal { get; set; }
        public virtual FormaPago FormaPago { get; set; }
        public virtual TipoJornada TipoJornada { get; set; }
        public virtual TipoPeriocidadPago TipoPeriocidadPago { get; set; }
        public virtual Employee Jefe { get; set; }

        public virtual ICollection<AhorroVoluntario> AhorrosVoluntario { get; set; }
        public virtual ICollection<AhorroWise> AhorrosWise { get; set; }
        public virtual ICollection<Employee> Dependientes {get;set;}
        public virtual ICollection<Incidencia> Incidencias { get; set; }
        public virtual ICollection<Nomina> Nominas { get; set; }
        public virtual ICollection<Prestamo> Prestamos { get; set; }
        public virtual ICollection<RegistroAsistencia> RegistroAsistencias { get; set; }
        public virtual ICollection<Ticket> TicketsAsignados { get; set; }
        public virtual ICollection<Ticket> TicketsCreados { get; set; }

        
    }

    
}
