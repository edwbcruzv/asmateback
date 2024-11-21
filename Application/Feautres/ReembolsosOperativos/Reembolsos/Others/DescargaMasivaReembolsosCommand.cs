using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Others
{
    public class DescargaMasivaReembolsosCommand : IRequest<Response<string>>
    {
        public int[] Ids { get; set; }

        public class Handler : IRequestHandler<DescargaMasivaReembolsosCommand,Response<string>>
        {
            private readonly IReembolsoService _reembolsoService;
            public Handler(IReembolsoService reembolsoService)
            {
                _reembolsoService = reembolsoService;
            }
            public Task<Response<string>> Handle(DescargaMasivaReembolsosCommand request, CancellationToken cancellationToken)
            {
                //int[] aux = { 22, 23, 46 };
                return _reembolsoService.DescargaMasivaReembolsos(request.Ids);
            }
        }
    }
}
