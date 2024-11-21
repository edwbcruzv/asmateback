using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class NominasByCompanyIdSpecification : Specification<Nomina>
    {
        public NominasByCompanyIdSpecification(int CompanyId, int PeriodoId)
        {
            Query.Where(x => x.CompanyId.Equals(CompanyId) && x.PeriodoId.Equals(PeriodoId));
        }
    }
}
