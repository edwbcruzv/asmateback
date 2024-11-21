using Ardalis.Specification;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Prestamos
{
    public class PrestamoByEmployeeIdAndIsActivoSpecification : Specification<Prestamo>
    {
        public PrestamoByEmployeeIdAndIsActivoSpecification(int Id)
        {
            Query.Where(x => x.EmployeeId == Id && x.Estatus == EstatusOperacion.Activo && x.FechaEstatusActivo != null);
        }
    }
}
