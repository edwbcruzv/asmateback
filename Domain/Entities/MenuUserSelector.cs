using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class MenuUserSelector : AuditableBaseEntity
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual User User { get; set; }
    }
}
