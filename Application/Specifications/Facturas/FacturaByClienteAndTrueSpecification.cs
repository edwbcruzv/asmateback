using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Facturas
{
    public class FacturaByClienteAndTrueSpecification : Specification<Factura>
    {
        public FacturaByClienteAndTrueSpecification(int ClientId)
        {
            Query.Where(x => x.ClientId == ClientId && x.Estatus == 1);
        }
    }
}
