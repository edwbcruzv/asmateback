using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class SubMenu : AuditableBaseEntity
    {
        public string SubMenuName { get; set; }
        public string SubMenuSource { get; set; }
        public string SubMenuIcon { get; set; }
        public int SubMenuOrder { get; set; }
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual ICollection<SubMenuUserSelector> SubMenuUserSelectors { get; set; }
    }
}
