using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Comprobantes
{
    public class ComprobanteByViaticoIdSpecification : Specification<Comprobante>
    {
        public ComprobanteByViaticoIdSpecification(int Id)
        {
            Query.Where(x => x.ViaticoId == Id);
        }
    }
}
