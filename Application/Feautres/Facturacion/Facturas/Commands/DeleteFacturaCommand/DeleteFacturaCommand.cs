using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.Facturas.Commands.DeleteFacturaCommand
{
    public class DeleteFacturaCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<DeleteFacturaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Factura> _repositoryAsync;

        public Handler(IRepositoryAsync<Factura> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteFacturaCommand request, CancellationToken cancellationToken)
        {
            var Factura = await _repositoryAsync.GetByIdAsync(request.Id);

            if (Factura == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(Factura);

                return new Response<int>(Factura.Id);
            }
        }
    }
}
