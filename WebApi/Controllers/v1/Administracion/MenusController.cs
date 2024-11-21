using Application.Feautres.Administracion.Menus.Queries.GetAllMenu;
using Application.Feautres.Administracion.Menus.Queries.GetMenuById;
using Application.Feautres.Administracion.Menus.Commands.DeleteMenuCommand;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Administracion.Menus.Queries.GetAllMenuByUser;
using Microsoft.AspNetCore.Authorization;
using Application.Feautres.Administracion.Menus.Commands.CreateMenuCommand;
using Application.Feautres.Administracion.Menus.Commands.UpdateMenuCommand;

namespace WebApi.Controllers.v1.Administracion
{
    [ApiVersion("1.0")]

    public class MenusController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateMenuCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateMenuCommand command)
        {
            if (command.Id != id)
                BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteMenuCommand { Id = id })); ;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetMenuByIdQuery { Id = id })); ;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await Mediator.Send(new GetAllMenuQuery { })); ;
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(int id)
        {
            return Ok(await Mediator.Send(new GetAllMenuByUserQuery { Id = id })); ;
        }
    }
}
