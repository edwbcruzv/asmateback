using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Prestamos.Queries.GetPrestamoFiles
{
    public class GetPrestamoPagarePDFByIdQuery : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetPrestamoPagarePDFByIdQuery, Response<string>>
        {
            private readonly IPrestamoService _prestamoService;
            public Handler(IPrestamoService prestamoService)
            {
                _prestamoService = prestamoService;
            }
            public async Task<Response<string>> Handle(GetPrestamoPagarePDFByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Response<string>(await _prestamoService.PagarePDF(request.Id), null);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new Response<string>("Error al encontrar documento");
                }
            }
        }
    }
}
