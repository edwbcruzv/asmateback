using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Employees
{
    public class EmployeeByNoEmpleadoAndNotIdSpecification : Specification<Employee>
    {
        public EmployeeByNoEmpleadoAndNotIdSpecification(Int64 noEmpleado, int Id)
        {
            Query.Where(x => x.NoEmpleado == noEmpleado && x.Id != Id);
        }
    }
}
