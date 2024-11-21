using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Menu : AuditableBaseEntity
    {
        public string MenuName { get; set; }
        public string MenuSource { get; set; }
        public string MenuIcon { get; set; }
        public string MenuOrder { get; set; }

        public virtual ICollection<MenuUserSelector> MenuUserSelectors { get; set; }
        public virtual ICollection<SubMenu> SubMenus { get; set; }
    }
}
