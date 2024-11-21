using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.Nominas.Commands.EnviarCorreoNominaCommand
{
    public class EnviarCorreoNominaCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<EnviarCorreoNominaCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Nomina> _repositoryAsyncNomina;
        private readonly ISendMailService _sendMail;

        public Handler(IRepositoryAsync<Nomina> repositoryAsyncNomina, ISendMailService sendMail)
        {
            _repositoryAsyncNomina = repositoryAsyncNomina;
            _sendMail = sendMail;
        }

        public async Task<Response<bool>> Handle(EnviarCorreoNominaCommand request, CancellationToken cancellationToken)
        {

            var nomina = await _repositoryAsyncNomina.GetByIdAsync(request.Id);
            Response<bool> response;
            if (nomina == null)
            {
                throw new KeyNotFoundException($"Nómina con Id: {request.Id} no encontrada en Nominas");
            }
            else
            {
                response = await _sendMail.sendNominaEmail(request.Id);
            }

            return response;

        }
    }
}
