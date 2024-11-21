using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.MiPortal.Commands.PdfEstadoDeCuentaWiseCommand
{
    public class PdfEstadoDeCuentaWiseCommand : IRequest<Response<string>>
    {
        public int AhorroWiseId { get; set; }
        public int Periodo { get; set; }
        public int CompanyId { get; set; }
    }
    public class Handler : IRequestHandler<PdfEstadoDeCuentaWiseCommand, Response<string>>
    {
        private readonly IPdfService _pdfService;

        public Handler(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<Response<string>> Handle(PdfEstadoDeCuentaWiseCommand request, CancellationToken cancellationToken)
        {
            var pdfString = await _pdfService.PdfEstadoDeCuentaWise(request.AhorroWiseId, request.Periodo, request.CompanyId);
            Response<string> response = new();
            response.Data = pdfString;
            response.Succeeded = true;
            return response;
        }
    }
}
