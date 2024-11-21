using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Companies
{
    public class CompaniesByEmployeeIdSpecification : Specification<Company>
    {
        public CompaniesByEmployeeIdSpecification(int employee_id)
        {
            Query.Where(x => x.Employees.Any(employee => employee.Id == employee_id ));
        }
    }
}
