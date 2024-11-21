using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Administracion
{
    public class DepartamentoByCompanyIdSpecification : Specification<Departamento>
    {
        public DepartamentoByCompanyIdSpecification(int Id)
        {
            Query.Where(x => x.CompanyId.Equals(Id));
        }
    }
}
