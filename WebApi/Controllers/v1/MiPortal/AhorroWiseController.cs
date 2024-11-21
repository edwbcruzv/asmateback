using Application.Feautres.MiPortal.AhorrosWise.Commands.CreateAhorroWiseCommand;
using Application.Feautres.MiPortal.AhorrosWise.Commands.DeleteAhorroWiseCommand;
using Application.Feautres.MiPortal.AhorrosWise.Commands.UpdateAhorroWiseCommand;
using Application.Feautres.MiPortal.AhorrosWise.Queries.GetAhorroWiseById;
using Application.Feautres.MiPortal.AhorrosWise.Queries.GetAllAhorrosWise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.MiPortal
{
    [ApiVersion("1.0")]
    public class AhorroWiseController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateAhorroWiseCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetAhorroWiseByIdQuery { Id = id }));
        }

        [HttpGet("employee/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByEmployeeId(int id)
        {
            return Ok(await Mediator.Send(new GetAllAhorrosWiseByEmployeeIdQuery { Id = id }));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch(UpdateAhorroWiseCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteAhorroWiseCommand { Id = id }));
        }
    }

}
