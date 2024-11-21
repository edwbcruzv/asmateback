using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Viaticos
{
    public class ViaticoByEmployeeIdSpecification : Specification<Viatico>
    {
        public ViaticoByEmployeeIdSpecification(int id)
        {
            Query.Where(x => x.EmployeeId ==  id);
        }
    }
}
