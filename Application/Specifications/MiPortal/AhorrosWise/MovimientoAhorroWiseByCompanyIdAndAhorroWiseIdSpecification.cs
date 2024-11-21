using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosWise
{
    public class MovimientoAhorroWiseByCompanyIdAndAhorroWiseIdSpecification : Specification<MovimientoAhorroWise>
    {
        public MovimientoAhorroWiseByCompanyIdAndAhorroWiseIdSpecification(int company_id, int ahorro_wise_id)
        {
            Query.Where(x => x.AhorroWiseId == ahorro_wise_id && x.CompanyId == company_id);
        }
    }
}
