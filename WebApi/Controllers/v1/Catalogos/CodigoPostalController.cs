using Application.Feautres.Catalogos.CodigoPostales.Queries.GetCodigoPostalByCodigoPostal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Catalogos
{
    public class CodigoPostalController : BaseApiController
    {
        [HttpGet("{codigoPostal}")]
        [Authorize]
        public async Task<IActionResult> GetAll(string codigoPostal)
        {
            return Ok(await Mediator.Send(new GetCodigoPostalByCodigoPostalQuery { codigoPostalId = codigoPostal }));
        }


    }
}
