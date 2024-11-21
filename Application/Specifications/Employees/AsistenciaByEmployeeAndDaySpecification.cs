using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Employees
{
    public class AsistenciaByEmployeeAndDaySpecification : Specification<RegistroAsistencia>
    {
        public AsistenciaByEmployeeAndDaySpecification(int EmployeeId, DateTime Dia)
        {
            Query.Where(x => x.EmployeeId == EmployeeId && x.Dia == Dia);
        }
    }
}
