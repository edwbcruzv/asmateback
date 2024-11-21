using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class TipoOtroPagoByClaveSpecification : Specification<TipoOtroPago>
    {
        public TipoOtroPagoByClaveSpecification(string clave)
        {
            Query.Where(x => x.Clave.Trim() == clave);
        }
    }
}
