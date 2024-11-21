using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class RegimenFiscalByClaveSpecification : Specification<RegimenFiscal>
    {
        public RegimenFiscalByClaveSpecification(string clave)
        {
            Query.Where(x => x.RegimenFiscalCve.Trim() == clave);
        }
    }
}
