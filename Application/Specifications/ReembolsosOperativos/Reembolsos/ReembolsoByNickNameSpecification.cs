using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReembolsosOperativos.Reembolsos
{
    public class ReembolsoByNickNameSpecification : Specification<Reembolso>
    {
        public ReembolsoByNickNameSpecification(string nickname)
        {
            Query.Where(x => x.CreatedBy.Equals(nickname));
        }
    }
}
