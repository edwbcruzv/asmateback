using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Administracion
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SalaryDays { get; set; }
        public string CompanyStatus { get; set; }
        public string? CompanyProfile { get; set; }
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
        public string? Calle { get; set; }
        public string? NoExt { get; set; }
        public string? NoInt { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
    }
}