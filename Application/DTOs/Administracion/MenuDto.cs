using Domain.Common;
using System;
using System.Collections.Generic;

namespace Application.DTOs.Administracion
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuSource { get; set; }
        public string MenuIcon { get; set; }
        public string MenuOrder { get; set; }

    }
}
