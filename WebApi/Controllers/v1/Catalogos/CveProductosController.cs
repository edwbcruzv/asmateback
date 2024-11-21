using Application.Feautres.Catalogos.CveProductos.Commands.UpdateCveProductosCommand;
using Application.Feautres.Catalogos.CveProductos.Queries.GetCveProductoAll;
using Application.Feautres.Catalogos.CveProductos.Queries.GetCveProductoByEstatus;
using Application.Feautres.Catalogos.CveProductos.Queries.GetCveProductoById;
using Application.Feautres.Catalogos.UnidadMedidas.Commands.UpdateUnidadMedidasCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class CveProductosController : BaseApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetCveProductoByEstatusQuery { }));
        }

        [HttpGet("getAll")]
        [Authorize]
        public async Task<IActionResult> GetAllCve()
        {
            return Ok(await Mediator.Send(new GetCveProductoAllQuery { }));
        }

        [HttpGet("cveProductos/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCveProductoByIdQuery { Id = id }));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateCveProductosCommand command)
        {

            if (command.Id != id)
                BadRequest();

            Console.WriteLine(command.Id);

            return Ok(await Mediator.Send(command));

        }


    }
}
