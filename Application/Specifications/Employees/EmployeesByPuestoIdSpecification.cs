using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Employees
{
    public class EmployeesByPuestoIdSpecification : Specification<Employee>
    {
        public EmployeesByPuestoIdSpecification(int Id)
        {
            Query.Where(x => x.PuestoId == Id);
        }
    }
}
