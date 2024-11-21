using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Kanban.Tickets
{
    public class TicketByCompanyIdSpecification : Specification<Ticket>
    {
        public TicketByCompanyIdSpecification(int company_id)
        {
            Query.Where(x => x.CompanyId == company_id);
        }
    }
}
