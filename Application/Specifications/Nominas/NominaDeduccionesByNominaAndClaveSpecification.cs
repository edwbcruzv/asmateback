using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class NominaDeduccionesByNominaAndClaveSpecification : Specification<NominaDeduccion>
    {
        public NominaDeduccionesByNominaAndClaveSpecification(int nomina_id, string clave)
        {
            Query.Where(x => x.NominaId == nomina_id &&  x.Clave == clave);
        }
    }
}
