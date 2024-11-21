using Application.Feautres.Administracion.SolicitudesDePlanes.Commands.ModificarEstatusSolicitudDePlanesCommand;
using Application.Feautres.Administracion.SolicitudesDePlanes.Queries.GetPrestamosYAhorrosPorCompania;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Administracion
{
    [ApiVersion("1.0")]
    public class SolicitudesDePlanesController : BaseApiController
    {
        [HttpGet("{CompanyId}")]
        [Authorize]
        public async Task<IActionResult> GetPrestamosYAhorrosPorCompania(int CompanyId)
        {
            return Ok(await Mediator.Send(new GetPrestamosYAhorrosPorCompaniaCommand { CompanyId = CompanyId }));
        }

        [HttpPut("ModificarEstatus")]
        [Authorize]
        public async Task <IActionResult> ModificarEstatus(ModificarEstatusSolicitudDePlanesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
