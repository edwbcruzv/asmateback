using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Facturas
{
    public class ComplementoPagoFacturaByComplementoPagoSpecification : Specification<ComplementoPagoFactura>
    {
        public ComplementoPagoFacturaByComplementoPagoSpecification(int ComplementoPagoId)
        {
            Query.Where(x => x.ComplementoPagoId == ComplementoPagoId);
        }
    }
}
