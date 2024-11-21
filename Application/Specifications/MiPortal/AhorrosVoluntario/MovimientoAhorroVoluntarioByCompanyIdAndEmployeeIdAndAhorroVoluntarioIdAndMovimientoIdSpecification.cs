using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosVoluntario
{
    public class MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdAndMovimientoIdSpecification : Specification<MovimientoAhorroVoluntario>
    {
        public MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdAndMovimientoIdSpecification(int company_id, int employee_id, int ahorro_voluntario_id,  int movimiento_id)
        {
            Query.Where(x => x.AhorroVoluntarioId == ahorro_voluntario_id && x.EmployeeId == employee_id && x.CompanyId == company_id && x.MovimientoId == movimiento_id);
        }
    }
}
