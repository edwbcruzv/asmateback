using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Administracion
{
    public class AsistenciaResumenDto
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public bool? SePagaPtu { get; set; }
        public bool? SePagaAguinaldo { get; set; }
        public bool? SePagaNomina { get; set; }
        public bool? SePagaIncapacidad { get; set; }
        public int Cantidad { get; set; }

    }
}
