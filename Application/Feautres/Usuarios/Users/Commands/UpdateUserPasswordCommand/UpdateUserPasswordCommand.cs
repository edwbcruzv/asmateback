using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Usuarios.Users.Commands.UpdateUserPasswordCommand
{
    public class UpdateUserPasswordCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string UserPasswordAux { get; set; }

    }
    public class Handler : IRequestHandler<UpdateUserPasswordCommand, Response<int>>
    {
        private readonly IRepositoryAsync<User> _repositoryAsync;
        private readonly IRsa _rsa;


        public Handler(IRepositoryAsync<User> repositoryAsync, IRsa rsa)
        {
            _repositoryAsync = repositoryAsync;
            _rsa = rsa;
        }

        public async Task<Response<int>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repositoryAsync.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {

                user.UserPassword = _rsa.Encript(request.UserPasswordAux);

                await _repositoryAsync.UpdateAsync(user);

                return new Response<int>(user.Id);



            }
        }
    }
}
