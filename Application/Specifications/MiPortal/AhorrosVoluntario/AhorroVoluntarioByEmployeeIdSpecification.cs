using Ardalis.Specification;
using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosVoluntario
{
    public class AhorroVoluntarioByEmployeeIdSpecification : Specification<AhorroVoluntario>
    {
        public AhorroVoluntarioByEmployeeIdSpecification(int Id)
        {
            Query.Where(x => x.EmployeeId == Id);
        }
    }
}
