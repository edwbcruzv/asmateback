using Application.Feautres.Catalogos.Estados.Queries.GetAllBancos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class EstadoController: BaseApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllEstadosQuery { }));
        }
    }
}
