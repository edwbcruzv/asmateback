using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReembolsosOperativos.MovimientoReembolsos
{
    public class MovimientoReembolsoByReembolsoIdSpecification : Specification<MovimientoReembolso>
    {
        public MovimientoReembolsoByReembolsoIdSpecification(int reembolso_id)
        {
            Query.Where(x => x.ReembolsoId == reembolso_id);
        }
    }
}
