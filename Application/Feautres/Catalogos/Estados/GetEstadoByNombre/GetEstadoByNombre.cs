using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Catalogos.Estados.GetEstadoByNombre
{
    public class GetEstadoByNombre : Specification<Estado>
    {
        public GetEstadoByNombre(string nombre) 
        {
            Query.Where(x => x.Nombre.Equals(nombre));
        }
    }
}
