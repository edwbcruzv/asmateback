using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Facturas
{
    public class FacturaByCompanyAndPPDAndTimbradoSpecification : Specification<Factura>
    {
        public FacturaByCompanyAndPPDAndTimbradoSpecification(int CompanyId, int ClientId)
        {

            //Posible error en ID METODO PAGO. . . . . . Cambiar Id ---->    Clave

            Query.Where(x => x.CompanyId == CompanyId && x.MetodoPagoId == 1 && x.Estatus == 2 && x.ClientId == ClientId);
        }
    }
}
