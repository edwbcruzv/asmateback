using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class ClientByUserSpecification : Specification<Client>
    {
        public ClientByUserSpecification(int id)
        {
            Query.Where(x => x.CompanyId == id);
        }
    }
}