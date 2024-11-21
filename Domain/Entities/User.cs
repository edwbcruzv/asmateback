using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class User : AuditableBaseEntity
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public byte[]? UserPassword { get; set; }
        public string UserProfile { get; set; }
        public string UserEmail { get; set; }
        public byte UserType { get; set; }

        public virtual ICollection<ContractsUserCompany> ContractsUserCompanies { get; set; }
        public virtual ICollection<MenuUserSelector> MenuUserSelectors { get; set; }
        public virtual ICollection<SubMenuUserSelector> SubMenuUserSelectors { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Reembolso> Reembolsos { get; set; }
    }
}
