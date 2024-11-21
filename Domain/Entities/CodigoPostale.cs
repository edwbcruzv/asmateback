using Domain.Common;

namespace Domain.Entities 
{
    public class CodigoPostale : AuditableBaseEntity
    {
        public string CodigoPostal { get; set; }
        public string Asentamiento { get; set; }
        public string TipoAsentamiento { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
    }
}
