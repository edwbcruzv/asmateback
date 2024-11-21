using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.EnviarCorreoEstadoDeCuentaWiseCommand
{
    public class EnviarCorreoEstadoDeCuentaWiseCommand : IRequest<Response<bool>>
    {
        public int EmployeeId { get; set; }
        public int Periodo { get; set; }
    }
    public class Handler : IRequestHandler<EnviarCorreoEstadoDeCuentaWiseCommand, Response<bool>>
    {
        private readonly ISendMailService _sendMailService;

        public Handler(ISendMailService sendMailService, ISendMailService sendMail)
        {
            _sendMailService = sendMailService;
        }

        public async Task<Response<bool>> Handle(EnviarCorreoEstadoDeCuentaWiseCommand request, CancellationToken cancellationToken)
        {
            Response<bool> response;
            response = await _sendMailService.SendEstadoDeCuentaAhorroWise(request.EmployeeId, request.Periodo);

            return response;

        }
    }
}
