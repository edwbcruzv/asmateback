using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.Incidencias
{
    public class DiasIncidenciaDto
    {
        public int diasVacaciones { get; set; }
        public int diasAdministrativos { get; set; }
        public int permisosSalirTemprano { get; set; }
        public int permisosLlegarTarde { get; set; }
        public int diasIncapacidad { get; set; }
    }
}
