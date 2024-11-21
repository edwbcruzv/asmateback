using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Prestamos
{
    public class MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdSpecification : Specification<MovimientoPrestamo>
    {
        public MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdSpecification(int company_id, int employee_id, int prestamo_id)
        {
            Query.Where(x => x.PrestamoId == prestamo_id && x.EmployeeId == employee_id && x.CompanyId == company_id);
        }
    }
}
