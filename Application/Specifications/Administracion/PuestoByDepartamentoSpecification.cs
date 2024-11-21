using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Administracion
{
    public class PuestoByDepartamentoSpecification : Specification<Puesto>
    {
        public PuestoByDepartamentoSpecification(int Id)
        {
            Query.Where(x => x.DepartamentoId.Equals(Id));
        }
    }
}
