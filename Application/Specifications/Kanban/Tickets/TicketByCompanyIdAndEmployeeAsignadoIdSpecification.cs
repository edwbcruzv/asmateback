using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Kanban.Tickets
{
    public class TicketByCompanyIdAndEmployeeAsignadoIdSpecification : Specification<Ticket>
    {
        public TicketByCompanyIdAndEmployeeAsignadoIdSpecification(int company_id, int employee_id)
        {
            Query.Where(x => x.CompanyId == company_id && x.EmployeeAsignadoId == employee_id);
        }
    }
}
