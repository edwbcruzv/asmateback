using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal
{
    public class IncidenciasByEmployeeIdSpecification : Specification<Incidencia>
    {
        public IncidenciasByEmployeeIdSpecification(int employeeId)
        {
            Query.Where(x => x.EmpleadoId.Equals(employeeId));
        }
    }
}
