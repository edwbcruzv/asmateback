using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Administracion.Companies.Commands.DeleteCompanyCommand;
using Application.Feautres.Administracion.Companies.Queries.GetCompanyById;
using Application.Feautres.Administracion.Companies.Queries.GetAllCompanyQuery;
using Microsoft.AspNetCore.Authorization;
using Application.Feautres.Usuarios.ContractsUserCompanies.Queries.GetContractsUserCompanyByUser;
using Application.Feautres.Usuarios.ContractsUserCompanies.Commands.CreateContractsUserCompanies;
using Application.Feautres.Usuarios.ContractsUserCompanies.Commands.DeleteContractsUserCompanies;
using Application.Feautres.Administracion.Companies.Commands.CreateCompanyCommand;
using Application.Feautres.Administracion.Companies.Commands.UpdateCompanyCommand;
using Application.Feautres.Administracion.Companies.Queries.GetAllCompaniesByEmployeeId;

namespace WebApi.Controllers.v1.Administracion
{
    [ApiVersion("1.0")]

    public class CompaniesController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] CreateCompanyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromForm] UpdateCompanyCommand command)
        {
            if (command.Id == id)
                BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCompanyCommand { Id = id })); ;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCompanyByIdQuery { Id = id })); ;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCompanyQuery())); ;
        }

        [HttpGet("employee/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllByEmployeeId(int id)
        {
            return Ok(await Mediator.Send(new GetAllCompaniesByEmployeeIdQuery { EmployeeId = id })); ;
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(int id)
        {
            return Ok(await Mediator.Send(new GetContractsUserCompanyByUserQuery { UserId = id })); ;
        }

        [HttpPost("{companyId}/user/{userid}")]
        [Authorize]
        public async Task<IActionResult> CreateContractsUserCompany(int companyId, int userId)
        {
            return Ok(await Mediator.Send(new CreateContractsUserCompanyCommand { CompanyId = companyId, UserId = userId }));
        }

        [HttpDelete("{companyId}/user/{userid}")]
        [Authorize]
        public async Task<IActionResult> DeleteContractsUserCompany(int companyId, int userId)
        {
            return Ok(await Mediator.Send(new DeleteContractsUserCompanyCommand { CompanyId = companyId, UserId = userId }));
        }

    }
}
