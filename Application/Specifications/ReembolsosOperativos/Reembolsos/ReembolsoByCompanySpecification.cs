using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReembolsosOperativos.Reembolsos
{
    public class ReembolsoByCompanySpecification : Specification<Reembolso>
    {
        public ReembolsoByCompanySpecification(int Id)
        {
            Query.Where(x => x.CompanyId.Equals(Id));
        }
    }
}
