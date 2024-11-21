using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosVoluntario
{
    public class MovimientoAhorroVoluntarioByEmployeeIdAndAhorroVoluntarioIdSpecification : Specification<MovimientoAhorroVoluntario>
    {
        public MovimientoAhorroVoluntarioByEmployeeIdAndAhorroVoluntarioIdSpecification( int employee_id, int ahorro_voluntario_id)
        {
            Query.Where(x => x.AhorroVoluntarioId == ahorro_voluntario_id && x.EmployeeId == employee_id);
        }
    }
}
