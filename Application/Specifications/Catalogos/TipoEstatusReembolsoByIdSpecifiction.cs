using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Catalogos
{
    public class TipoEstatusReembolsoByIdSpecifiction : Specification<TipoEstatusReembolso>
    {
        public TipoEstatusReembolsoByIdSpecifiction(int id)
        {
            Query.Where(x => x.Id.Equals(id));
        }
    }
}
