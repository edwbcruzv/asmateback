using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Facturas
{
    public class FacturaByCompanyAndTrueSpecification : Specification<Factura>
    {
        public FacturaByCompanyAndTrueSpecification(int CompanyId)
        {
            Query.Where(x => x.CompanyId == CompanyId && x.Estatus == 1);
        }
    }
}
