using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReembolsosOperativos.MovimientoReembolsos
{
    public class MovimientoReembolsoByIdSpecification : Specification<MovimientoReembolso>
    {
        public MovimientoReembolsoByIdSpecification(int movReembolsoId)
        {
            Query.Where(x => x.Id == movReembolsoId);
        }
    }
}
