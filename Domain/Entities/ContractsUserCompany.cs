using Domain.Common;

namespace Domain.Entities
{
    public partial class ContractsUserCompany : AuditableBaseEntity
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
