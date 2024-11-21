using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Usuarios.Users.Commands.DeleteUserCommand
{
    public class DeleteUserCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<DeleteUserCommand, Response<int>>
    {
        private readonly IRepositoryAsync<User> _repositoryAsync;

        public Handler(IRepositoryAsync<User> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repositoryAsync.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(user);

                return new Response<int>(user.Id);
            }
        }
    }
}
