using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal
{
    public class IncidenciasByCompanyIdSpecification : Specification<Incidencia>
    {
        public IncidenciasByCompanyIdSpecification(int companyId)
        {
            Query.Where(x => x.CompanyId.Equals(companyId));
            
        }
    }
}
