using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Companies
{
    public class CompaniesByIdAndEstadoSpecification : Specification<Company>
    {
        public CompaniesByIdAndEstadoSpecification(int id_company, string estado)
        {
            Query.Where(x => x.Estado == estado && x.Id == id_company);
        }
    }
}
