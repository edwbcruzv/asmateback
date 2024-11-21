using Application.Feautres.Facturacion.Facturas.Commands.TimbrarFacturaCommand;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.Nominas.Commands.TimbrarNominaCommand
{
    public class TimbrarNominaCommand: IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<TimbrarNominaCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Nomina> _repositoryAsyncNomina;
        private readonly ITimboxService _timboxPruebas;
        
        public Handler(IRepositoryAsync<Nomina> repositoryAsync, ITimboxService timboxPruebas)
        {
            _repositoryAsyncNomina = repositoryAsync;
            _timboxPruebas = timboxPruebas;
        }
        public IRepositoryAsync<Nomina> RepositoryAsync => _repositoryAsyncNomina;
        public async Task<Response<bool>> Handle(TimbrarNominaCommand request, CancellationToken cancellationToken)
        {
            var timbrado = await _timboxPruebas.timbrarNomina(request.Id);
            
            return timbrado;
        }
    }
}
