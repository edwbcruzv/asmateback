using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Comprobantes
{
    public class ComprobanteByUuidSpecification : Specification<Comprobante>
    {
        public ComprobanteByUuidSpecification(string Uuid) 
        {
            Query.Where(x => x.Uuid.Equals(Uuid));
        }
    }
}
