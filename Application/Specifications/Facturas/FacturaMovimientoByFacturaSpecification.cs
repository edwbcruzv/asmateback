using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Facturas
{
    public class FacturaMovimientoByFacturaSpecification : Specification<FacturaMovimiento>
    {
        public FacturaMovimientoByFacturaSpecification(int Id)
        {
            Query.Where(x => x.FacturaId.Equals(Id));
        }
    }
}
