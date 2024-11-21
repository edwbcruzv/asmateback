using Application.Feautres.Usuarios.Users.Queries.GetAllUser;
using Application.Feautres.Usuarios.Users.Queries.GetUserById;
using Application.Feautres.Usuarios.Users.Commands.DeleteUserCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Usuarios.Users.Commands.CreateUserCommand;
using Application.Feautres.Usuarios.Users.Commands.UpdateUserCommand;
using Application.Feautres.Usuarios.Users.Commands.UpdateUserPasswordCommand;

namespace WebApi.Controllers.v1.Usuarios
{
    [ApiVersion("1.0")]

    public class UsersController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromForm] UpdateUserCommand command)
        {
            if (command.Id != id)
                BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpPut("password/{id}")]
        [Authorize]
        public async Task<IActionResult> PutPassword(int id, UpdateUserPasswordCommand command)
        {
            if (command.Id != id)
                BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand { Id = id })); ;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id })); ;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await Mediator.Send(new GetAllUserQuery { })); ;
        }
    }
}
