using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Employees
{
    public class EmployeeByCompanySpecification : Specification<Employee>
    {
        public EmployeeByCompanySpecification(int Id)
        {
            Query.Where(x => x.CompanyId.Equals(Id));
        }
    }
}
