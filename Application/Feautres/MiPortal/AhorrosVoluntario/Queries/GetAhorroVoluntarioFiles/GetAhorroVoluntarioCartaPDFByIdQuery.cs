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
    public class GetAhorroVoluntarioCartaPDFByIdQuery : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetAhorroVoluntarioCartaPDFByIdQuery, Response<string>>
        {
            private readonly IAhorroVoluntarioService _ahorroVoluntarioService;

            public Handler(IAhorroVoluntarioService ahorroVoluntario)
            {
                _ahorroVoluntarioService = ahorroVoluntario;
            }

            public async Task<Response<string>> Handle(GetAhorroVoluntarioCartaPDFByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Response<string>(await _ahorroVoluntarioService.CartaPDF(request.Id), null);

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
