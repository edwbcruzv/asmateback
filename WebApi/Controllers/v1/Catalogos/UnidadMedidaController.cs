using Application.Feautres.Catalogos.UnidadMedidas.Commands.UpdateUnidadMedidasCommand;
using Application.Feautres.Catalogos.UnidadMedidas.Queries.GetUnidadMedidaAll;
using Application.Feautres.Catalogos.UnidadMedidas.Queries.GetUnidadMedidaByEstatus;
using Application.Feautres.Catalogos.UnidadMedidas.Queries.GetUnidadMedidaById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class UnidadMedidaController : BaseApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTrue()
        {
            return Ok(await Mediator.Send(new GetUnidadMedidaByEstatusQuery { }));
        }

        [HttpGet("getAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetUnidadMedidaAllQuery { }));
        }


        [HttpGet("UnidadMedida/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetUnidadMedidaByIdQuery { Id = id }));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id,UpdateUnidadMedidasCommand command)
        {

            if (command.Id != id)
                BadRequest();

            Console.WriteLine(command.Id);

            return Ok(await Mediator.Send(command));

        }


    }
}
