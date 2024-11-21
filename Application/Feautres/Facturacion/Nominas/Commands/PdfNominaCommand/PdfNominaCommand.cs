using Application.DTOs.Facturas;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.Nominas.Commands.PdfNominaCommand
{
    public class PdfNominaCommand: IRequest<Response<NominaPdfDto>>
    {
        public int Id { get; set; }
    }
    public class Handler: IRequestHandler<PdfNominaCommand, Response<NominaPdfDto>>
    {
        private readonly IRepositoryAsync<Nomina> _repositoryAsyncNomina;
        private readonly IPdfService _pdfService;

        public Handler(IRepositoryAsync<Nomina> repositoryAsyncNomina, IPdfService pdfService)
        {
            _repositoryAsyncNomina = repositoryAsyncNomina;
            _pdfService = pdfService;
        }

        public IRepositoryAsync<Nomina> RepositoryAsyncNomina => _repositoryAsyncNomina;

        public async Task<Response<NominaPdfDto>> Handle(PdfNominaCommand request, CancellationToken cancellationToken)
        {
            return await _pdfService.PdfNomina(request.Id);
        }
    }
}
