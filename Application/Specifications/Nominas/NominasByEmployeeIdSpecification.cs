using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class NominasByEmployeeIdSpecification : Specification<Nomina>
    {
        public NominasByEmployeeIdSpecification(int EmployeeId)
        {
            Query.Where(x => x.EmployeeId.Equals(EmployeeId));
        }
    }
}
