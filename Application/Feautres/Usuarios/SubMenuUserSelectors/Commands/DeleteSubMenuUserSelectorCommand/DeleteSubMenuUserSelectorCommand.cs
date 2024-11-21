using Application.Interfaces;
using Application.Specifications.SubMenu;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Usuarios.SubMenuUserSelectors.Commands.DeleteSubMenuUserSelectorCommand
{
    public class DeleteSubMenuUserSelectorCommand : IRequest<Response<int>>
    {
        public int submenu { get; set; }
        public int user { get; set; }
    }
    public class Handler : IRequestHandler<DeleteSubMenuUserSelectorCommand, Response<int>>
    {
        private readonly IRepositoryAsync<SubMenuUserSelector> _repositoryAsync;

        public Handler(IRepositoryAsync<SubMenuUserSelector> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteSubMenuUserSelectorCommand request, CancellationToken cancellationToken)
        {
            var SubMenuUserSelector = await _repositoryAsync.FirstOrDefaultAsync(new SubMenuUserSelectorBySubmenuAndUserSpecification(request.submenu, request.user));

            if (SubMenuUserSelector == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el usuario {request.user} y submenu {request.submenu}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(SubMenuUserSelector);

                return new Response<int>(SubMenuUserSelector.Id);
            }
        }
    }
}
