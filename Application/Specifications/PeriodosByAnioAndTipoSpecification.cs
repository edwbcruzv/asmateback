using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications
{
    public class PeriodosByAnioAndTipoAndCompanySpecification : Specification<Periodo>
    {
        public PeriodosByAnioAndTipoAndCompanySpecification(int CompanyId, int Anio, int tipoPeriocidadId)
        {
            Query.Where(x => (x.TipoPeriocidadPagoId == tipoPeriocidadId) && (x.CompanyId == CompanyId) && (x.Desde.Year == Anio ) );
        }
    }
}
