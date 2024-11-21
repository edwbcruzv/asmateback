using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Catalogos
{
    public class TipoComprobanteByClaveSpecification : Specification<TipoComprobante>
    {
        public TipoComprobanteByClaveSpecification(string clave)
        {
            Query.Where(x => x.TipoDeComprobante.Equals(clave));
        }
    }
}
