using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosVoluntario
{
    public class MovimientoAhorroVoluntarioByCompanyIdAndAhorroVoluntarioIdSpecification : Specification<MovimientoAhorroVoluntario>
    {
        public MovimientoAhorroVoluntarioByCompanyIdAndAhorroVoluntarioIdSpecification( int company_id, int ahorro_voluntario_id)
        {
            Query.Where(x => x.AhorroVoluntarioId == ahorro_voluntario_id && x.CompanyId == company_id);
        }
    }
}
