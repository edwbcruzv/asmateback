using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Prestamos
{
    public class PrestamoByEmployeeIdSpecification : Specification<Prestamo>
    {
        public PrestamoByEmployeeIdSpecification(int Id)
        {
            Query.Where(x => x.EmployeeId == Id);
        }
    }
}
