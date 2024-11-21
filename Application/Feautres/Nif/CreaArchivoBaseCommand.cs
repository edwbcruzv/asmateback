using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Nif
{
    public class CreaArchivoBaseCommand : IRequest<Response<String>>
    {
        public class Handler : IRequestHandler<CreaArchivoBaseCommand, Response<String>>
        {
            private readonly IExcelService _excelService;
            public Handler(IExcelService excelService)
            {
                _excelService = excelService;
            }

            public Task<Response<String>> Handle(CreaArchivoBaseCommand request, CancellationToken cancellationToken)
            {
                return _excelService.CreateExcelCalculoNif();
            }
        }
    }
}
