using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class SalarioMinimoByAnioSpecification : Specification<SalarioMinimo>
    {
        public SalarioMinimoByAnioSpecification(int anio)
        {
            Query.Where(x => x.Anio == anio);
        }
    }
}
