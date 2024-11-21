using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosWise
{
    public class MovimientoAhorroWiseByCompanyIdAndEmployeeIdAndAhorroWiseIdAndMovimientoIdSpecification : Specification<MovimientoAhorroWise>
    {
        public MovimientoAhorroWiseByCompanyIdAndEmployeeIdAndAhorroWiseIdAndMovimientoIdSpecification(int company_id, int employee_id, int ahorro_wise_id, int movimiento_id)
        {
            Query.Where(x => x.AhorroWiseId == ahorro_wise_id && x.EmployeeId == employee_id && x.CompanyId == company_id && x.MovimientoId == movimiento_id);
        }
    }
}
