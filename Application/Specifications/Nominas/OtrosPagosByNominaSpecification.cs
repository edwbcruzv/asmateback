using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Nominas
{
    public class OtrosPagosByNominaSpecification : Specification<NominaOtroPago>
    {
        public OtrosPagosByNominaSpecification(int Id) 
        {
            Query.Where(x => x.NominaId.Equals(Id));
        }
    }
}
