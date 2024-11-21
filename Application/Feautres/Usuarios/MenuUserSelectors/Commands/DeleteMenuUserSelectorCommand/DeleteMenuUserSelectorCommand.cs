using Application.Interfaces;
using Application.Specifications.MenuUser;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Usuarios.MenuUserSelectors.Commands.DeleteMenuUserSelectorCommand
{
    public class DeleteMenuUserSelectorCommand : IRequest<Response<int>>
    {
        public int menu { get; set; }
        public int user { get; set; }
    }
    public class Handler : IRequestHandler<DeleteMenuUserSelectorCommand, Response<int>>
    {
        private readonly IRepositoryAsync<MenuUserSelector> _repositoryAsync;

        public Handler(IRepositoryAsync<MenuUserSelector> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteMenuUserSelectorCommand request, CancellationToken cancellationToken)
        {
            var MenuUserSelector = await _repositoryAsync.FirstOrDefaultAsync(new MenuUserSelectorByMenuAndUserSpecification(request.menu, request.user));

            if (MenuUserSelector == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el usuario {request.user} y menu {request.menu}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(MenuUserSelector);

                return new Response<int>(MenuUserSelector.Id);
            }
        }
    }
}
