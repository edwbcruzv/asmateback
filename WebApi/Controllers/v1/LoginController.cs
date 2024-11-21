using Application.Feautres.Authtenticate.Commands.AuthenticateCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class LoginController : BaseApiController
    {
        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult> UserAuthentication(AuthenticateCommand login)
        {
            var init = await Mediator.Send(login);

            return Ok(init);
        }
    }
}
