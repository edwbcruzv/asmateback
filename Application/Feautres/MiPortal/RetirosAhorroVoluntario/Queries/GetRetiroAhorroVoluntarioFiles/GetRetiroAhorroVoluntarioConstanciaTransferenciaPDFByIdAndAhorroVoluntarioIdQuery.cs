using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Queries.GetRetiroAhorroVoluntarioFiles
{
    public class GetRetiroAhorroVoluntarioConstanciaTransferenciaPDFByIdAndAhorroVoluntarioIdQuery : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int AhorroVoluntarioId { get; set; }

        public class Handler : IRequestHandler<GetRetiroAhorroVoluntarioConstanciaTransferenciaPDFByIdAndAhorroVoluntarioIdQuery, Response<string>>
        {
            private readonly IRetiroAhorroVoluntarioService _retiroAhorroVoluntarioService;

            public Handler(IRetiroAhorroVoluntarioService retiroAhorroVoluntarioService)
            {
                _retiroAhorroVoluntarioService = retiroAhorroVoluntarioService;
            }

            public async Task<Response<string>> Handle(GetRetiroAhorroVoluntarioConstanciaTransferenciaPDFByIdAndAhorroVoluntarioIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Response<string>(await _retiroAhorroVoluntarioService.ConstanciaTransferenciaPDF(request.Id, request.AhorroVoluntarioId));

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
