using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class PeriodosByEtapaAndDesdeAndHastaSpecification : Specification<Periodo>
    {
        public PeriodosByEtapaAndDesdeAndHastaSpecification(int etapa,DateTime desde,DateTime hasta,int companyId )
        {
            Query.Where(x => x.Etapa == etapa && x.Desde == desde && x.Hasta == hasta && x.CompanyId == companyId);
        }

    }
}
