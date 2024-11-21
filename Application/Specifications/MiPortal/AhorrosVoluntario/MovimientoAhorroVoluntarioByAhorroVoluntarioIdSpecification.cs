using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosVoluntario
{
    public class MovimientoAhorroVoluntarioByAhorroVoluntarioIdSpecification : Specification<MovimientoAhorroVoluntario>
    {
        public MovimientoAhorroVoluntarioByAhorroVoluntarioIdSpecification(int ahorro_voluntario_id)
        {
            Query.Where(x => x.AhorroVoluntarioId == ahorro_voluntario_id);
        }
    }
}
