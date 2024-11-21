using System;
using System.Collections.Generic;

namespace Application.DTOs.Catalogos
{
    public partial class TipoMonedaDto
    {
        public int Id { get; set; }
        public string Pais { get; set; }
        public string Modena { get; set; }
        public string CodigoIso { get; set; }
        public string? Culture { get; set; }

    }
}
