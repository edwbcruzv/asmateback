using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Administracion.Menus.Commands.DeleteMenuCommand
{
    public class DeleteMenuCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<DeleteMenuCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Menu> _repositoryAsync;

        public Handler(IRepositoryAsync<Menu> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var Menu = await _repositoryAsync.GetByIdAsync(request.Id);

            if (Menu == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(Menu);

                return new Response<int>(Menu.Id);
            }
        }
    }
}
