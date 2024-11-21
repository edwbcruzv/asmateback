using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Client : AuditableBaseEntity
    {
        public int CompanyId { get; set; }
        public string Rfc { get; set; }
        public string Name { get; set; }
        public string RazonSocial { get; set; }
        public string? Calle { get; set; }
        public string? NoExt { get; set; }
        public string? NoInt { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
        public string CodigoPostal { get; set; }
        public bool? Estatus { get; set; }
        public short TipoPersona { get; set; }
        public int RegimenFiscalId { get; set; }
        public string? Clabe { get; set; }
        public string Correos { get; set; }

        public virtual Company Company { get; set; }
        public virtual RegimenFiscal RegimenFiscal { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPagos { get; set; }

    }
}
