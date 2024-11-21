using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Usuarios
{
    public class SubMenuUserRelationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? MenuId { get; set; }
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuSource { get; set; }
        public string SubMenuIcon { get; set; }
        public int SubMenuOrder { get; set; }
    }
}