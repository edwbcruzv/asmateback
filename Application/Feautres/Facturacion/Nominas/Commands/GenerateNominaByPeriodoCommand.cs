using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.Nominas.Commands
{
    public class GenerateNominaByPeriodoCommand : IRequest<Response<bool>>
    {
        public int PeriodoId { get; set; }

    }

    public class Handler : IRequestHandler<GenerateNominaByPeriodoCommand, Response<bool>>
    {
        public INominaService _nominaService;

        public Handler(INominaService nominaService)
        {
            _nominaService = nominaService;
        }

        public Task<Response<bool>> Handle(GenerateNominaByPeriodoCommand request, CancellationToken cancellationToken)
        {
            return _nominaService.generateNominaByPeriodo(request.PeriodoId);
        }
    }
}
