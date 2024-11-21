using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReembolsosOperativos.MovimientoReembolsos
{
    public class MovimientoReembolsoByUuidSpecification : Specification<MovimientoReembolso>
    {
        public MovimientoReembolsoByUuidSpecification(string uuid)
        {
            Query.Where(x => x.Uuid.Equals(uuid));
        }
    }
}
