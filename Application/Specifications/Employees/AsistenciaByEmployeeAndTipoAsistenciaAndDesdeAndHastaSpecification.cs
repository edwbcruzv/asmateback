using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Employees
{
    public class AsistenciaByEmployeeAndTipoAsistenciaAndDesdeAndHastaSpecification : Specification<RegistroAsistencia>
    {
        public AsistenciaByEmployeeAndTipoAsistenciaAndDesdeAndHastaSpecification(int EmployeeId, int TipoAsistenciaId, DateTime Desde, DateTime Hasta)
        {
            Query.Where(x => x.EmployeeId == EmployeeId && x.TipoAsistenciaId == TipoAsistenciaId && ( x.Dia >= Desde && x.Dia <= Hasta ));
        }
    }
}
