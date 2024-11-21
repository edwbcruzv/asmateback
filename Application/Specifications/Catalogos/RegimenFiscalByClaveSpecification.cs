using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class UnidadMedidaByEstatusSpecification : Specification<UnidadMedida>
    {
        public UnidadMedidaByEstatusSpecification(bool status)
        {
            Query.Where(x => x.Estatus == status);
        }
    }
}
