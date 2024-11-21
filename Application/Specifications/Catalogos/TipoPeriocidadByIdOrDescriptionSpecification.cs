using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Catalogos
{
    public class TipoPeriocidadByIdOrDescriptionSpecification : Specification <TipoPeriocidadPago>
    {
        public TipoPeriocidadByIdOrDescriptionSpecification(int id)
        {
            Query.Where(x=> x.Id.Equals(id));
        }
        public TipoPeriocidadByIdOrDescriptionSpecification(string descripcion)
        {
            Query.Where(x => x.Descripcion.Equals(descripcion));
        }
    }
}
