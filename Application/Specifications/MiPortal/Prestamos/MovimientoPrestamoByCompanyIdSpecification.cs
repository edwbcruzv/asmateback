using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Prestamos
{
    public class MovimientoPrestamoByCompanyIdSpecification : Specification<MovimientoPrestamo>
    {
        public MovimientoPrestamoByCompanyIdSpecification( int company_id)
        {
            Query.Where(x => x.CompanyId == company_id);
        }
    }
}
