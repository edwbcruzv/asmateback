using Application.DTOs.Facturas;
using Application.Feautres.Facturacion.Facturas.Commands.CancelarFacturaCommand;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.Nominas.Commands.CancelarNominaCommnad
{
    public class CancelarNominaCommand : IRequest<Response<EstatusCancelacionDto>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<CancelarNominaCommand, Response<EstatusCancelacionDto>>
    {
        private readonly IRepositoryAsync<Nomina> _repositoryAsyncNomina;
        private readonly ITimboxService _timbox;

        public Handler(IRepositoryAsync<Nomina> repositoryAsync, ITimboxService timboxPruebas)
        {
            _repositoryAsyncNomina = repositoryAsync;
            _timbox = timboxPruebas;
        }
        public IRepositoryAsync<Nomina> RepositoryAsync => _repositoryAsyncNomina;

        public async Task<Response<EstatusCancelacionDto>> Handle(CancelarNominaCommand request, CancellationToken cancellationToken)
        {
            return await _timbox.cancelarNomina(request.Id);
        }

    }
}
