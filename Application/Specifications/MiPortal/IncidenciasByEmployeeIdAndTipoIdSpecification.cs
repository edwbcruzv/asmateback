using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal
{
    public class IncidenciasByEmployeeIdAndTipoIdSpecification : Specification<Incidencia>
    {
        public IncidenciasByEmployeeIdAndTipoIdSpecification(int employeeid,int tipoId)
        {
            Query.Where(x => x.EmpleadoId == employeeid && x.TipoId == tipoId);
        }
    }
}
