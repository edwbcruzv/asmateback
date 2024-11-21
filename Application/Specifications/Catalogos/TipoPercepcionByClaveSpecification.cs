using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class TipoPercepcionByClaveSpecification : Specification<TipoPercepcion>
    {
        public TipoPercepcionByClaveSpecification(string clave)
        {
            Query.Where(x => x.Clave.Trim() == clave);
        }
    }
}
