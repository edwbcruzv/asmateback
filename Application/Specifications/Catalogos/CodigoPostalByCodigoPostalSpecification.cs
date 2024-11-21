using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class CodigoPostalByCodigoPostalSpecification : Specification<CodigoPostale>
    {
        public CodigoPostalByCodigoPostalSpecification(string CodigoPostal)
        {
            Query.Where(x => x.CodigoPostal == CodigoPostal);
        }
    }
}
