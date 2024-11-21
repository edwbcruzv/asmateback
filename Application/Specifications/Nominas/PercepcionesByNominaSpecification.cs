using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class PercepcionesByNominaSpecification : Specification<NominaPercepcion>
    {
        public PercepcionesByNominaSpecification(int Id)
        {
            Query.Where(x => x.NominaId.Equals(Id));
        }
    }
}
