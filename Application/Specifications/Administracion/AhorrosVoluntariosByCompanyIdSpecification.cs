using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Administracion
{
    public class AhorrosVoluntariosByCompanyIdSpecification : Specification<AhorroVoluntario>
    {
        public AhorrosVoluntariosByCompanyIdSpecification(int CompanyId)
        {
            Query.Where(x => x.CompanyId == CompanyId);
        }
    }
}
