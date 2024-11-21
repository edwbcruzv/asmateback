using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosVoluntario
{
    public class MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdSpecification : Specification<MovimientoAhorroVoluntario>
    {
        public MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdSpecification(int company_id, int employee_id)
        {
            Query.Where(x => x.EmployeeId == employee_id && x.CompanyId == company_id);
        }
    }
}
