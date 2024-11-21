using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosVoluntario.Queries.GetAllAhorrosVoluntario
{
    public class GetAhorroVoluntarioSolicitudRetiroPDFByIdQuery : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public float Cantidad { get; set; }
        public class Handler : IRequestHandler<GetAhorroVoluntarioSolicitudRetiroPDFByIdQuery, Response<string>>
        {
            private readonly IAhorroVoluntarioService _ahorroVoluntarioService;

            public Handler(IAhorroVoluntarioService ahorroVoluntario)
            {
                _ahorroVoluntarioService = ahorroVoluntario;
            }

            public async Task<Response<string>> Handle(GetAhorroVoluntarioSolicitudRetiroPDFByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Response<string>(await _ahorroVoluntarioService.SolicitudRetiroPDF(request.Id,request.Cantidad), null);

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
