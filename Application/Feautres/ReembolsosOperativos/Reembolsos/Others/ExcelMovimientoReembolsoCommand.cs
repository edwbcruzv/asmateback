using Application.Wrappers;
using MediatR;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Others
{
    public class ExcelMovimientoReembolsoCommand : IRequest<Response<string>>
    {
        public int Id { get;set; }
        public class Handler: IRequestHandler<ExcelMovimientoReembolsoCommand,Response<String>> 
        {
            private readonly IReembolsoService _reembolsoService;
            public Handler(IReembolsoService reembolsoService)
            {
                _reembolsoService = reembolsoService;
            }

            public Task<Response<string>> Handle(ExcelMovimientoReembolsoCommand request, CancellationToken cancellationToken)
            {
                return _reembolsoService.CrearExcelMovimientoReembolso(request.Id);
            }
        }
    }
}
