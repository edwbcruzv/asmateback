using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Prestamos
{
    public class MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdAndMovimientoIdSpecification : Specification<MovimientoPrestamo>
    {
        public MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdAndMovimientoIdSpecification(int company_id, int employee_id, int prestamo_id,int movimiento_id)
        {
            Query.Where(x => x.PrestamoId == prestamo_id && x.EmployeeId == employee_id && x.CompanyId == company_id && x.MovimientoId == movimiento_id);
        }
    }
}
