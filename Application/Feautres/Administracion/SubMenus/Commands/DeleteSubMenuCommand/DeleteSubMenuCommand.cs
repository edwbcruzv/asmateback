using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Administracion.SubMenus.Commands.DeleteSubMenuCommand
{
    public class DeleteSubMenuCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<DeleteSubMenuCommand, Response<int>>
    {
        private readonly IRepositoryAsync<SubMenu> _repositoryAsync;

        public Handler(IRepositoryAsync<SubMenu> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteSubMenuCommand request, CancellationToken cancellationToken)
        {
            var SubMenu = await _repositoryAsync.GetByIdAsync(request.Id);

            if (SubMenu == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(SubMenu);

                return new Response<int>(SubMenu.Id);
            }
        }
    }
}
