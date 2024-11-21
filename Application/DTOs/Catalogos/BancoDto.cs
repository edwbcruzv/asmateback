using System;
using System.Collections.Generic;

namespace Application.DTOs.Catalogos
{
    public partial class BancoDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }
}
