using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Prestamos
{
    public class MovimientoPrestamoByEmployeeIdAndPrestamoIdSpecification : Specification<MovimientoPrestamo>
    {
        public MovimientoPrestamoByEmployeeIdAndPrestamoIdSpecification(int employee_id, int prestamo_id)
        {
            Query.Where(x => x.PrestamoId == prestamo_id && x.EmployeeId == employee_id);
        }
    }
}
