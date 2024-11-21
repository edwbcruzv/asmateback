using Ardalis.Specification;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.AhorrosVoluntario
{
    public class AhorroVoluntarioByEmployeeIdAndIsPendienteSpecification : Specification<AhorroVoluntario>
    {
        public AhorroVoluntarioByEmployeeIdAndIsPendienteSpecification(int Id)
        {
            Query.Where(x => x.EmployeeId == Id && x.Estatus == EstatusOperacion.Pendiente );
        }
    }
}
