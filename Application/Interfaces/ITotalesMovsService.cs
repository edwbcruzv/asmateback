using Application.DTOs.Facturas;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITotalesMovsService
    {
        public TotalesMovimientosDto getTotalesFormMovs(List<FacturaMovimiento> facturaMovimientos);

    }
}
