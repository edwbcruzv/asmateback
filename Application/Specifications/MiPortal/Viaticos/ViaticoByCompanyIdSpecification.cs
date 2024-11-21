using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.MiPortal.Viaticos
{
    public class ViaticoByCompanyIdSpecification : Specification<Viatico>
    {
        public ViaticoByCompanyIdSpecification(int Id)
        {
            Query.Where(x => x.CompanyId == Id);
        }
    }
}
