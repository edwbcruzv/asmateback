namespace Application.DTOs.Catalogos
{
    public class NominaDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string EmisorRazonSocial { get; set; }
        public int EmisorRegimenFistalId { get; set; }
        public string? LogoSrcCompany { get; set; }
        public string LugarExpedicion { get; set; }
        public int EmployeeId { get; set; }
        public string? ReceptorRazonSocial { get; set; }
        public string? ReceptorRfc { get; set; }
        public int? ReceptorRegimenFiscalId { get; set; }
        public int ReceptorUsoCfdiId { get; set; }
        public string? ReceptorDomicilioFiscal { get; set; }
        public int? TipoMonedaId { get; set; }
        public int MetodoPagoId { get; set; }
        public int TipoPeriocidadPagoId { get; set; }
        public int PuestoId { get; set; }
        public int TipoContratoId { get; set; }
        public int TipoJornadaId { get; set; }
        public int TipoRegimenId { get; set; }
        public int TipoRiesgoTrabajoId { get; set; }
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
        public short Estatus { get; set; }
        public short TipoNomina { get; set; }
        public int? Folio { get; set; }
        public int? TipoDeduccionId { get; set; }
        public int? TipoPercepcionId { get; set; }
        public DateTime Created { get; set; }
        public string RegistroPatronal { get; set; }
        public int DiasPago { get; set; }
        public int? TipoOtroPagoId { get; set; }
        public double? Total { get; set; }
        public int Periodo { get; set; }
    }
}
