using Application.DTOs.Facturas;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Modulo para obtener los totales de los movimientos en una póliza*/

namespace Shared.Services
{
    public class TotalesMovsService : ITotalesMovsService
    {


        public TotalesMovimientosDto getTotalesFormMovs(List<FacturaMovimiento> facturaMovimientos)
        {
            var tMDto = new TotalesMovimientosDto();

            foreach (var fmTemp in facturaMovimientos)
            {

                decimal totalMovimiento = fmTemp.Cantidad * fmTemp.PrecioUnitario;
                tMDto.subTotal += totalMovimiento;
                tMDto.descuentoTotal += fmTemp.Descuento;

                if (fmTemp.ObjetoImpuestoId == 2)
                {
                    if (fmTemp.Iva)
                    {
                        tMDto.iva = (totalMovimiento - 0) * 0.16m;
                        tMDto.trasladadosTotal += tMDto.iva;
                        tMDto.baseIva += totalMovimiento;
                        tMDto.tieneTraslados = true;
                    }
                    if (fmTemp.Iva6)
                    {
                        tMDto.iva6 = (totalMovimiento - 0) * 0.06m;
                        tMDto.retenidosTotal += tMDto.iva6;
                        tMDto.retencionIva6Total += tMDto.iva6;
                        tMDto.tieneRetencionIva6 = true;
                    }
                    if (fmTemp.RetencionIsr)
                    {
                        tMDto.retencionISR = (totalMovimiento - 0) * 0.10m;
                        tMDto.retenidosTotal += tMDto.retencionISR;
                        tMDto.retencionIsrTotal += tMDto.retencionISR;
                        tMDto.tieneRetencionIsr = true;
                    }
                    if (fmTemp.RetencionIva)
                    {
                        tMDto.retencionIva = (totalMovimiento - 0) * 0.1067m;
                        tMDto.retenidosTotal += tMDto.retencionIva;
                        tMDto.retencionIvaTotal += tMDto.retencionIva;
                        tMDto.tieneRetencionIva = true;
                    }
                }
            }

            tMDto.total = tMDto.subTotal + tMDto.trasladadosTotal - tMDto.retenidosTotal - tMDto.descuentoTotal;

            return tMDto;

        }

      
    }

    

}
