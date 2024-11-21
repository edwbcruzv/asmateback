using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Feautres.Facturacion.Nominas.Commands.ReporteNominaByPeriodoCommand
{
    public class ReporteNominaByPeriodoCommand : IRequest<Response<SourceFileDto>>
    {
        public int Id { get; set; }

    }
    public class Handler : IRequestHandler<ReporteNominaByPeriodoCommand, Response<SourceFileDto>>
    {

        private readonly IExcelService _excelService;

        public Handler(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public async Task<Response<SourceFileDto>> Handle(ReporteNominaByPeriodoCommand request, CancellationToken cancellationToken)
        {

            var response = await _excelService.CreateExcelReporteDeNominas(request.Id);

            return response;

        }


    }
}
