using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.ContracsUsers
{
    public class ContractsUserCompanyByUserSpecification : Specification<ContractsUserCompany>
    {
        public ContractsUserCompanyByUserSpecification(int id)
        {
            Query.Where(x => x.UserId == id);
        }
    }
}
