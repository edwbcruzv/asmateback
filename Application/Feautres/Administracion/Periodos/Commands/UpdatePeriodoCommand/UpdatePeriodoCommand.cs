using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Administracion.Periodos.Commands.UpdatePeriodoCommand
{
    public class UpdatePeriodoCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<UpdatePeriodoCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Periodo> _repositoryAsyncPeriodo;

        public Handler(IRepositoryAsync<Periodo> repositoryAsyncPeriodo)
        {
            _repositoryAsyncPeriodo = repositoryAsyncPeriodo;
        }

        public async Task<Response<bool>> Handle(UpdatePeriodoCommand request, CancellationToken cancellationToken)
        {
            // Verificar si el periodo a actualizar existe antes de intentar actualizarlo
            var periodo = await _repositoryAsyncPeriodo.GetByIdAsync(request.Id);
            if (periodo == null)
            {
                throw new KeyNotFoundException($"El periodo con Id {request.Id} no fue encontrado.");
            }

            periodo.Estatus = (periodo.Estatus == 0) ? 1 : 0;

            // Realizar la actualización del periodo
            await _repositoryAsyncPeriodo.UpdateAsync(periodo);

            return new Response<bool>(true);
        }
    }
}
