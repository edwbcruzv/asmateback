using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Facturas
{
    public class ComplementoPagoByCompanySpecification : Specification<ComplementoPago>
    {
        public ComplementoPagoByCompanySpecification(int CompanyId)
        {
            Query.Where(x => x.CompanyId == CompanyId);
        }
    }
}
