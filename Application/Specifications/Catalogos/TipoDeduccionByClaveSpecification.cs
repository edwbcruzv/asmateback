using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class TipoDeduccionByClaveSpecification : Specification<TipoDeduccion>
    {
        public TipoDeduccionByClaveSpecification(string clave)
        {
            Query.Where(x => x.Clave.Trim() == clave);
        }
    }
}
