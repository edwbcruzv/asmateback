using System;
using System.Collections.Generic;

namespace Application.DTOs.Administracion
{
    public partial class DepartamentoDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Company { get; set; }

    }
}
