using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class DeduccionesByNominaSpecification : Specification<NominaDeduccion>
    {
        public DeduccionesByNominaSpecification(int Id)
        {
            Query.Where(x => x.NominaId.Equals(Id));
        }
    }
}
