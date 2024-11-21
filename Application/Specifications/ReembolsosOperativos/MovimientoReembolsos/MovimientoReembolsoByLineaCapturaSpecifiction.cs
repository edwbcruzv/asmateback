using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReembolsosOperativos.MovimientoReembolsos
{
    public class MovimientoReembolsoByLineaCapturaSpecifiction : Specification<MovimientoReembolso>
    {
        public MovimientoReembolsoByLineaCapturaSpecifiction(string linea_captura)
        {
            Query.Where(x => x.LineaCaptura.Equals(linea_captura));
        }
    }
}
