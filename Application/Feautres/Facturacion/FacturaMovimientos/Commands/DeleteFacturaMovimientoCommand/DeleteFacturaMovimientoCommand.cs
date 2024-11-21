using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.FacturaMovimientos.Commands.DeleteFacturaMovimientoCommand
{
    public class DeleteFacturaMovimientoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<DeleteFacturaMovimientoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<FacturaMovimiento> _repositoryAsync;

        public Handler(IRepositoryAsync<FacturaMovimiento> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteFacturaMovimientoCommand request, CancellationToken cancellationToken)
        {
            var FacturaMovimiento = await _repositoryAsync.GetByIdAsync(request.Id);

            if (FacturaMovimiento == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(FacturaMovimiento);

                return new Response<int>(FacturaMovimiento.Id);
            }
        }
    }
}
