using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Administracion.Employees.Commands.DeleteEmployeeCommand
{
    public class DeleteEmployeeCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<DeleteEmployeeCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;

        public Handler(IRepositoryAsync<Employee> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var Employee = await _repositoryAsync.GetByIdAsync(request.Id);

            if (Employee == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(Employee);

                return new Response<int>(Employee.Id);
            }
        }
    }
}
