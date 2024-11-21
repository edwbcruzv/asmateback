using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Nominas
{
    public class MostRecentSalaryByNominaIDSpecification : Specification<NominaPercepcion>
    {
        public MostRecentSalaryByNominaIDSpecification(int nominaID)
        {
            Query.Where(x => x.NominaId.Equals(nominaID) && x.Clave.Equals("001"));
        }
    }
}
