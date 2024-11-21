using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Employees
{
    public class EmployeeByCompanyAndEstatusAnTipoPeriocidadSpecification : Specification<Employee>
    {
        public EmployeeByCompanyAndEstatusAnTipoPeriocidadSpecification(int Id, int Estatus, int TipoPeriocidadPagoId)
        {
            Query.Where(x => x.CompanyId.Equals(Id) && x.Estatus == Estatus && x.TipoPeriocidadPagoId == TipoPeriocidadPagoId);
        }
    }
}
