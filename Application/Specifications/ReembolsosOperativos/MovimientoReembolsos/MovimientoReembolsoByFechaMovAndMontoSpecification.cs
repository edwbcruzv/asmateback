using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.ReembolsosOperativos.MovimientoReembolsos
{
    public class MovimientoReembolsoByFechaMovAndTotalSpecification : Specification<MovimientoReembolso>
    {
        public MovimientoReembolsoByFechaMovAndTotalSpecification(DateTime fecha_movimiento,double total)
        {
            Query.Where(x => x.FechaMovimiento.Equals(fecha_movimiento) && x.Total == total);
        }
    }
}
