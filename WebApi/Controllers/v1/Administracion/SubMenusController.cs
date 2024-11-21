using Application.Feautres.Administracion.SubMenus.Queries.GetAllSubMenu;
using Application.Feautres.Administracion.SubMenus.Queries.GetSubMenuById;
using Application.Feautres.Administracion.SubMenus.Commands.DeleteSubMenuCommand;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Administracion.SubMenus.Queries.GetAllSubMenuByUser;
using Microsoft.AspNetCore.Authorization;
using Application.Feautres.Administracion.SubMenus.Commands.CreateSubMenuCommand;
using Application.Feautres.Administracion.SubMenus.Commands.UpdateSubMenuCommand;

namespace WebApi.Controllers.v1.Administracion
{
    [ApiVersion("1.0")]

    public class SubMenusController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateSubMenuCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateSubMenuCommand command)
        {
            if (command.Id != id)
                BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteSubMenuCommand { Id = id })); ;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetSubMenuByIdQuery { Id = id })); ;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await Mediator.Send(new GetAllSubMenuQuery { })); ;
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(int id)
        {
            return Ok(await Mediator.Send(new GetAllSubMenuByUserQuery { Id = id })); ;
        }
    }
}
