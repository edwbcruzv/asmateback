using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Catalogos
{
    public class TipoEstatusReembolsoByDescripcionSpecifiction : Specification<TipoEstatusReembolso>
    {
        public TipoEstatusReembolsoByDescripcionSpecifiction(string descripcion)
        {
            Query.Where(x => x.Descripcion.Equals(descripcion));
        }
    }
}
