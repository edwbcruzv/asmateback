using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Company : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SalaryDays { get; set; }
        public string CompanyStatus { get; set; }
        public string CompanyProfile { get; set; }
        public string? RegistroPatronal { get; set; }
        public string? PostalCode { get; set; }
        public int RegimenFiscalId { get; set; }
        public string? RepresentanteLegal { get; set; }
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public string? Certificado { get; set; }
        public string? PrivateKeyFile { get; set; }
        public string? PassPrivateKey { get; set; }
        public int? FolioFactura { get; set; }
        public int? FolioNomina { get; set; }
        public string? Calle { get; set; }
        public string? NoExt { get; set; }
        public string? NoInt { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }

        public virtual RegimenFiscal RegimenFiscal { get; set; }
        //public virtual CompanyBiometricsDatum CompanyBiometricsDatum { get; set; }
        //public virtual ICollection<Biometric> Biometrics { get; set; }


        public virtual ICollection<AhorroVoluntario> AhorrosVoluntario { get; set; }
        public virtual ICollection<AhorroWise> AhorrosWise { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPagos { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<ContractsUserCompany> ContractsUserCompanies { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Departamento> Departamentos { get; set; }
        public virtual ICollection<Periodo> Periodos { get; set; }
        public virtual ICollection<Prestamo> Prestamos { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Nomina> Nominas { get; set; }
        public virtual ICollection<Reembolso> Rembolsos { get; set; }
        //public virtual ICollection<Vacation> Vacations { get; set; }
        public virtual ICollection<Nif> Nifs { get; set; }
        public virtual ICollection<Incidencia> Incidencias { get; set; }
    }
}
