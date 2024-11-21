using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class CveProductoByEstatusSpecification : Specification<CveProducto>
    {
        public CveProductoByEstatusSpecification(bool status)
        {
            Query.Where(x => x.Estatus == status);
        }
    }
}
