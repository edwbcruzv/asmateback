using Domain.Common;
using System;
using System.Collections.Generic;

namespace Application.DTOs.Administracion
{
    public class SubMenuDto
    {
        public int Id { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuSource { get; set; }
        public string SubMenuIcon { get; set; }
        public int SubMenuOrder { get; set; }
        public int MenuId { get; set; }

    }
}
