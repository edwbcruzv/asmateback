using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class TipoMonedaByClaveSpecification : Specification<TipoMoneda>
    {
        public TipoMonedaByClaveSpecification(string clave)
        {
            Query.Where(x => x.CodigoIso.Equals(clave));
        }
    }
}
