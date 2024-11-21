using Application.Feautres.MiPortal.Viaticos.Commands.CreateViaticoCommand;
using Application.Feautres.MiPortal.Viaticos.Commands.DeleteViaticoCommand;
using Application.Feautres.MiPortal.Viaticos.Commands.UpdateViaticoCommand;
using Application.Feautres.MiPortal.Viaticos.Commands.UpdateEstatusViatico;
using Application.Feautres.MiPortal.Viaticos.Queries.GetViaticoById;
using Application.Feautres.MiPortal.Viaticos.Queries.GetViaticosByCompanyId;
using Application.Feautres.MiPortal.Viaticos.Queries.GetViaticosByEmployeeId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.MiPortal
{

    [ApiVersion("1.0")]
    public class ViaticoController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateViaticoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetViaticoByIdQuery { Id = id }));
        }

        [HttpGet("employee/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByEmployeeId(int id)
        {
            return Ok(await Mediator.Send(new GetViaticosByEmployeeIdQuery { Id = id }));
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByCompanyId(int id)
        {
            return Ok(await Mediator.Send(new GetViaticosByCompanyIdQuery { Id = id }));
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult> Patch(UpdateViaticoCommand command)
        {
            //if (id != command.Id) return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteViaticoCommand { Id = id }));
        }

        [HttpPut("estatus")]
        [Authorize]
        public async Task<ActionResult> PutEstatus(UpdateEstatusViaticoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
