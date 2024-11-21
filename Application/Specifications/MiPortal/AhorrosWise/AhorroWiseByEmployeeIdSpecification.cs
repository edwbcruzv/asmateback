using Ardalis.Specification;
using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosWise
{
    public class AhorroWiseByEmployeeIdSpecification : Specification<AhorroWise>
    {
        public AhorroWiseByEmployeeIdSpecification(int Id)
        {
            Query.Where(x => x.EmployeeId == Id);
        }
    }
}
