using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosWise
{
    public class MovimientoAhorroWiseByCompanyIdSpecification : Specification<MovimientoAhorroWise>
    {
        public MovimientoAhorroWiseByCompanyIdSpecification(int company_id)
        {
            Query.Where(x => x.CompanyId == company_id);
        }
    }
}
