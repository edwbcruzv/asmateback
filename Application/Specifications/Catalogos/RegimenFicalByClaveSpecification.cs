using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Catalogos
{
    public class RegimenFicalByClaveSpecification : Specification<RegimenFiscal>
    {
        public RegimenFicalByClaveSpecification(string clave)
        {
            Query.Where(x => x.RegimenFiscalCve.Equals(clave));
        }

        public RegimenFicalByClaveSpecification(int clave)
        {
            Query.Where(x => x.RegimenFiscalCve.Equals(clave.ToString()));
        }
    }
}
