using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Employees
{
    public class EmployeeByNoEmpleadoSpecification : Specification<Employee>
    {
        public EmployeeByNoEmpleadoSpecification(Int64 Id)
        {
            Query.Where(x => x.NoEmpleado == Id);
        }
    }
}
