using Application.DTOs.Catalogos;
using Application.Interfaces;
using Application.Specifications;
using Application.Specifications.Employees;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.Nominas.Commands.GetNominasByUserIdCommand
{
    public class GetNominasByUserIdCommand : IRequest<Response<List<NominaDTO>>>
    {
        public int UserId { get; set; }
    }
    public class Handler : IRequestHandler<GetNominasByUserIdCommand, Response<List<NominaDTO>>>
    {
        private readonly IRepositoryAsync<Nomina> _repositoryAsyncNomina;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
        private readonly IRepositoryAsync<User> _repositoryAsyncUser;
        private readonly IRepositoryAsync<Periodo> _repositoryAsyncPeriodo;
        public INominaService _nominaService;
        private readonly IMapper _mapper;

        public Handler(
            IRepositoryAsync<Nomina> repositoryAsyncNomina,
            IRepositoryAsync<Employee> repositoryAsyncEmployee,
            IRepositoryAsync<User> repositoryAsyncUser,
            IRepositoryAsync<Periodo> repositoryAsyncPeriodo,
            INominaService nominaService,
            IMapper mapper)
        {
            _repositoryAsyncNomina = repositoryAsyncNomina;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
            _repositoryAsyncUser = repositoryAsyncUser;
            _repositoryAsyncPeriodo = repositoryAsyncPeriodo;
            _nominaService = nominaService;
            _mapper = mapper;
        }

        public async Task<Response<List<NominaDTO>>> Handle(GetNominasByUserIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _repositoryAsyncUser.GetByIdAsync(request.UserId);
            var employee = await _repositoryAsyncEmployee.FirstOrDefaultAsync(new EmployeeByUserIdSpecification(user.Id));
            var nominas = await _repositoryAsyncNomina.ListAsync(new NominasByEmployeeIdSpecification(employee.Id));
            var copiaNominas = new List<NominaDTO>();
            foreach (var nomina in nominas)
            {
                Periodo periodo = await _repositoryAsyncPeriodo.GetByIdAsync(nomina.PeriodoId);
                var nominaDTO = _mapper.Map<NominaDTO>(nomina);
                nominaDTO.Total = await _nominaService.CalcularTotal(nomina);
                nominaDTO.Periodo = periodo.Etapa;
                copiaNominas.Add(nominaDTO);
            }
            var dtos = _mapper.Map<List<NominaDTO>>(copiaNominas);
            return new Response<List<NominaDTO>>(dtos);

        }
    }
}
