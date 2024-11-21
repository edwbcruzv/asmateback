using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class UmaByAnioSpecification : Specification<Uma>
    {
        public UmaByAnioSpecification(int anio)
        {
            Query.Where(x => x.Anio == anio);
        }
    }
}
