using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class PeriodosByCompanyAndTipoSpecification : Specification<Periodo>
    {
        public PeriodosByCompanyAndTipoSpecification(int companyId,int tipo)
        {
            Query.Where(x => x.CompanyId == companyId && x.TipoPeriocidadPagoId == tipo);
        }

    }
}
