using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Asistencias
{
    public class AsistenciasByClaveSpecification : Specification<TipoAsistencia>
    {
        public AsistenciasByClaveSpecification(string clave)
        {
            Query.Where(x => x.Clave.Equals(clave));
        }
    }
}
