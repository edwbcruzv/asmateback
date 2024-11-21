using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Nominas
{
    public class MostRecentNominaByEmployeeIDSpecification : Specification<Nomina>
    {
        public MostRecentNominaByEmployeeIDSpecification(int employeeId, int tipoPeriocidadPagoId)
        {
            Query.Where(x => x.EmployeeId.Equals(employeeId) && x.TipoPeriocidadPagoId.Equals(tipoPeriocidadPagoId)).OrderBy(x => x.Created).Take(1);
        }
    }
}
