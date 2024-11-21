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
    public class ComplementoPagoFacturaByFacturaSpecification : Specification<ComplementoPagoFactura>
    {
        public ComplementoPagoFacturaByFacturaSpecification(int FacturaId)
        {
            Query.Where(x => x.FacturaId == FacturaId);
        }
    }
}
