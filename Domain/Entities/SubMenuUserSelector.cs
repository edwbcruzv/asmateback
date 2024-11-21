using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class SubMenuUserSelector : AuditableBaseEntity
    {
        public int UserId { get; set; }
        public int SubMenuId { get; set; }
        public int? MenuId { get; set; } 
        public virtual SubMenu SubMenu { get; set; }
        public virtual User User { get; set; }
    }
}
