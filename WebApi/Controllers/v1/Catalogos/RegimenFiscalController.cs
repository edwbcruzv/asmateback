using Application.Feautres.Catalogos.RegimenFiscals.Queries.GetAllRegimenFiscal;
using Application.Feautres.Catalogos.RegimenFiscals.Queries.GetRegimenFiscalByEstatus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class RegimenFiscalController : BaseApiController
    {
        [HttpGet("regimenFiscal/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetRegimenFiscalByIdQuery { Id = id }));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllRegimenFiscalQuery { }));
        }


    }
}
