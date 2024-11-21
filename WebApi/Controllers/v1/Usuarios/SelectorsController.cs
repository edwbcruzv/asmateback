using Application.Feautres.Usuarios.MenuUserSelectors.Commands.DeleteMenuUserSelectorCommand;
using Application.Feautres.Usuarios.SubMenuUserSelectors.Commands.DeleteSubMenuUserSelectorCommand;
using Application.Feautres.Usuarios.MenuUserSelectors.Commands.CreateMenuUserSelectorCommand;
using Application.Feautres.Usuarios.SubMenuUserSelectors.Commands.CreateSubMenuUserSelectorsCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Usuarios
{

    public class SelectorsController : BaseApiController
    {

        [HttpPost("menu")]
        [Authorize]
        public async Task<IActionResult> PostMenu(CreateMenuUserSelectorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("submenu")]
        [Authorize]
        public async Task<IActionResult> PostSubMenu(CreateSubMenuUserSelectorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("menu/{menu}/user/{user}")]
        [Authorize]
        public async Task<IActionResult> HttpDeleteMenu(int menu, int user)
        {
            return Ok(await Mediator.Send(new DeleteMenuUserSelectorCommand { menu = menu, user = user }));
        }

        [HttpDelete("submenu/{submenu}/user/{user}")]
        [Authorize]
        public async Task<IActionResult> HttpDeleteSubMenu(int submenu, int user)
        {
            return Ok(await Mediator.Send(new DeleteSubMenuUserSelectorCommand { submenu = submenu, user = user }));
        }

    }

}
