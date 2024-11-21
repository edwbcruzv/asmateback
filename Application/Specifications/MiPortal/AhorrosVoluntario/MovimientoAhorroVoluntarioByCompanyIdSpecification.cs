using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosVoluntario
{
    public class MovimientoAhorroVoluntarioByCompanyIdSpecification : Specification<MovimientoAhorroVoluntario>
    {
        public MovimientoAhorroVoluntarioByCompanyIdSpecification(int company_id)
        {
            Query.Where(x => x.CompanyId == company_id);
        }
    }
}
