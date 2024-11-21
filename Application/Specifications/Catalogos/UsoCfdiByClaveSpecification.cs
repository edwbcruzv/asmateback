using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class UsoCfdiByClaveSpecification : Specification<UsoCfdi>
    {
        public UsoCfdiByClaveSpecification(string clave)
        {
            Query.Where(x => x.UsoDeCfdi.Trim() == clave);
        }
    }
}
