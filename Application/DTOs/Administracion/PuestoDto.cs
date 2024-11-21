using System;
using System.Collections.Generic;

namespace Application.DTOs.Administracion
{
    public partial class PuestoDto
    {
        public int Id { get; set; }
        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }

    }
}
