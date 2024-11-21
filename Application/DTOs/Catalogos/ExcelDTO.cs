using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Catalogos
{
    public class ExcelDTO
    {
        public IFormFile File { get; set; }
        public int CompanyId { get; set; }
        public string? NombreEjercicio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
