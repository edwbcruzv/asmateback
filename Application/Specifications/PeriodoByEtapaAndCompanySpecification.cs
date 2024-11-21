using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class PeriodoByEtapaAndCompanySpecification : Specification<Periodo>
    {
        public PeriodoByEtapaAndCompanySpecification(int etapa, int companyId)
        {
            Query.Where(x => x.Etapa == etapa && x.CompanyId == companyId);
        }
    }
}
