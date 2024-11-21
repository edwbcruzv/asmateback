using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class VacacionByAnioSpecification : Specification<Vacacion>
    {
        public VacacionByAnioSpecification(int anio, int CompanyId)
        {
            Query.Where(x => x.AnioLimiteInferior <= anio && x.AnioLimiteSuperior >= anio && x.CompanyId == CompanyId) ;
        }
    }
}
