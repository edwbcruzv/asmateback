using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Catalogos
{
    public class FormaPagoByClaveSpecification : Specification<FormaPago>
    {
        public FormaPagoByClaveSpecification(string clave)
        {
            Query.Where(x => x.FormaDePago.Equals(clave));
        }
    }
}
