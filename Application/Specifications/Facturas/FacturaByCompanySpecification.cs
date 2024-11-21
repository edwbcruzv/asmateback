using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Facturas
{
    public class FacturaByCompanySpecification : Specification<Factura>
    {
        public FacturaByCompanySpecification(int Id)
        {
            Query.Where(x => x.CompanyId.Equals(Id));
        }
    }
}
