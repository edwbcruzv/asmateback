using Application.Feautres.Administracion.Employees.Commands.DeleteEmployeeCommand;
using Application.Feautres.Administracion.Employees.Queries.GetAllEmployeeByCompany;
using Application.Feautres.Administracion.Employees.Queries.GetEmployeeById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Feautres.Administracion.Employees.Commands.CreateEmployeeCommand;
using Application.Feautres.Administracion.Employees.Commands.UpdateEmployeeCommand;
using Application.Feautres.Administracion.Employees.Queries.GetEmployeeByUserId;
using Application.Feautres.Administracion.Employees.Queries.GetAllEmployeesByDepartamentoId;

namespace WebApi.Controllers.v1.Administracion
{
    [ApiVersion("1.0")]

    public class EmployeeController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateEmployeeCommand command)
        {
            if (command.Id != id)
                BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {

            return Ok(await Mediator.Send(new DeleteEmployeeCommand { Id = id })); ;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {

            return Ok(await Mediator.Send(new GetEmployeeByIdQuery { Id = id })); ;
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByCompanyId(int id)
        {

            return Ok(await Mediator.Send(new GetAllEmployeeByCompanyQuery { CompanyId = id })); ;
        }

        [HttpGet("departamento/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByDepartamentoId(int id)
        {

            return Ok(await Mediator.Send(new GetAllEmployeesByDepartamentoIdQuery { DepartamentoId = id })); ;
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<ActionResult> EmpleadoPorUserId(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByUserIdQuery { UserId =  id })); ;
        }
    }
}
