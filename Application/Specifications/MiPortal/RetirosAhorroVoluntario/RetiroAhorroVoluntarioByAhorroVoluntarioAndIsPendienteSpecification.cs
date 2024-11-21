using Ardalis.Specification;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.RetirosAhorroVoluntario
{
    public class RetiroAhorroVoluntarioByAhorroVoluntarioAndIsPendienteSpecification : Specification<RetiroAhorroVoluntario>
    {
        public RetiroAhorroVoluntarioByAhorroVoluntarioAndIsPendienteSpecification(int ahorro_id)
        {
            Query.Where(x => x.AhorroVoluntarioId == ahorro_id && x.Estatus == EstatusRetiro.Pendiente);
        }
    }
}
