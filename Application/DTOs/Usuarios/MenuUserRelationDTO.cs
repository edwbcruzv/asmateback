using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Usuarios
{
    public class MenuUserRelationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuSource { get; set; }
        public string MenuIcon { get; set; }
        public string MenuOrder { get; set; }
    }
}