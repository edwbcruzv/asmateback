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
    public class RetiroAhorroVoluntarioByIdSpecification : Specification<RetiroAhorroVoluntario>
    {
        public RetiroAhorroVoluntarioByIdSpecification(int id)
        {
            Query.Where(x => x.Id == id );
        }
    }
}
