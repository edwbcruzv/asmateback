using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Administracion
{
    public class PrestamosByCompanyIdSpecification: Specification<Prestamo>
    {
        public PrestamosByCompanyIdSpecification(int CompanyId)
        {
            Query.Where(x=> x.CompanyId == CompanyId);
        }
    }
}
