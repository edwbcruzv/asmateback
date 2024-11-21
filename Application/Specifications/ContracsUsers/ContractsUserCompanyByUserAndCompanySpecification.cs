using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.ContracsUsers
{
    public class ContractsUserCompanyByUserAndCompanySpecification : Specification<ContractsUserCompany>
    {
        public ContractsUserCompanyByUserAndCompanySpecification(int CompanyId, int UserId)
        {
            Query.Where(x => x.UserId == UserId && x.CompanyId == CompanyId);
        }
    }
}
