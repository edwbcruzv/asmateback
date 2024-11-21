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
    public class GetPrestamoEstadoCuentaPDFByIdQuery : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetPrestamoEstadoCuentaPDFByIdQuery, Response<string>>
        {
            private readonly IPrestamoService _prestamoService;
            public Handler(IPrestamoService prestamoService)
            {
                _prestamoService = prestamoService;
            }
            public async Task<Response<string>> Handle(GetPrestamoEstadoCuentaPDFByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Response<string>(await _prestamoService.EstadoCuentaPDF(request.Id), null);

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
