using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Employees
{
    public class EmployeeByCompanyAndEstatusSpecification : Specification<Employee>
    {
        public EmployeeByCompanyAndEstatusSpecification(int Id, int Estatus)
        {
            Query.Where(x => x.CompanyId.Equals(Id) && x.Estatus == Estatus);
        }
    }
}
