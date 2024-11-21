using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Nomina : AuditableBaseEntity
    {
        public int CompanyId { get; set; }
        public string EmisorRazonSocial { get; set; }
        public int EmisorRegimenFistalId { get; set; }
        public string? LogoSrcCompany { get; set; }
        public string LugarExpedicion { get; set; }
        public string RegistroPatronal { get; set; }
        public int EmployeeId { get; set; }
        public string? ReceptorRazonSocial { get; set; }
        public string? ReceptorRfc { get; set; }
        public int? ReceptorRegimenFiscalId { get; set; }
        public int? ReceptorUsoCfdiId { get; set; }
        public string? ReceptorDomicilioFiscal { get; set; }
        public int? TipoMonedaId { get; set; }
        public int? MetodoPagoId { get; set; }
        public int? TipoPeriocidadPagoId { get; set; }
        public int? PuestoId { get; set; }
        public int? TipoContratoId { get; set; }
        public int? TipoJornadaId { get; set; }
        public int? TipoRegimenId { get; set; }
        public int? TipoRiesgoTrabajoId { get; set; }
        public int PeriodoId { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public DateTime? FechaTimbrado { get; set; }
        public string? Uuid { get; set; }
        public string? SelloCfdi { get; set; }
        public string? SelloSat { get; set; }
        public string? CadenaOriginal { get; set; }
        public string? NoCertificadoSat { get; set; }
        public string? FileXmlTimbrado { get; set; }
        /*
         * ESTATUS
         * 1: CREADO
         * 2: TIMBRADO
         * 3: CANCELADO
         */
        public short Estatus { get; set; }
        /*
         * TIPO NOMINA
         * 1: Normal
         * 2: Especial
         */
        public short TipoNomina { get; set; }
        public int? Folio { get; set; }
        public int DiasPago { get; set; }
        public int? TipoDeduccionId { get; set; }
        public int? TipoPercepcionId { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public double? SalarioDiario { get; set; }


        public virtual Company Company { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual RegimenFiscal RegimenFiscal { get; set; }
        public virtual UsoCfdi UsoCfdi { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual TipoPeriocidadPago TipoPeriocidadPago { get; set; }
        public virtual Puesto Puesto { get; set; }
        public virtual TipoContrato TipoContrato { get; set; }
        public virtual TipoJornada TipoJornada { get; set; }
        public virtual TipoRegimen TipoRegimen { get; set; }
        public virtual TipoRiesgoTrabajo TipoRiesgoTrabajo { get; set; }
        public virtual Periodo Periodo { get; set; }
        public virtual ICollection<NominaDeduccion>? NominaDeducciones { get; set; }
        public virtual ICollection<NominaOtroPago>? NominaOtroPagos { get; set; }
        public virtual ICollection<NominaPercepcion>? NominaPercepciones { get; set; }

    }
}
