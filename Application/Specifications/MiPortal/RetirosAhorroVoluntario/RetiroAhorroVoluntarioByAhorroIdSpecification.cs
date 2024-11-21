using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.RetirosAhorroVoluntario
{
    public class RetiroAhorroVoluntarioByAhorroIdSpecification : Specification<RetiroAhorroVoluntario>
    {
        public RetiroAhorroVoluntarioByAhorroIdSpecification(int id)
        {
            Query.Where(x => x.AhorroVoluntarioId == id);
        }
    }
}
