using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Catalogos
{
    public class IsrByAnioAndTipoAndMontoSpecification : Specification<Isr>
    {
        public IsrByAnioAndTipoAndMontoSpecification(int anio, int Periodo, double monto)
        {
            Query.Where(x => x.LimiteInferior <= monto && x.LimiteSuperior >= monto && x.Anio == anio && x.Periodo == Periodo) ;
        }
    }
}
