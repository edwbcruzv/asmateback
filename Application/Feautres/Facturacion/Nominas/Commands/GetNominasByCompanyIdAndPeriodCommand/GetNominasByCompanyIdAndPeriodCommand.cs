using Application.DTOs.Catalogos;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Facturacion.Nominas.Commands.GetNominasByCompanyIdAndPeriodCommand
{
    public class GetNominasByCompanyIdAndPeriodCommand : IRequest<Response<List<NominaDTO>>>
    {
        public int CompanyId { get; set; }
        public int PeriodoId { get; set; }
    }
    public class Handler : IRequestHandler<GetNominasByCompanyIdAndPeriodCommand, Response<List<NominaDTO>>>
    {
        private readonly IRepositoryAsync<Nomina> _repositoryAsync;
        private readonly IRepositoryAsync<Periodo> _repositoryAsyncPeriodo;
        private readonly IMapper _mapper;
        public INominaService _nominaService;

        public Handler(
            IRepositoryAsync<Nomina> repositoryAsync,
            IRepositoryAsync<Periodo> repositoryAsyncPeriodo,
            INominaService nominaService,
            IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _repositoryAsyncPeriodo = repositoryAsyncPeriodo;
            _nominaService = nominaService;
            _mapper = mapper;
        }

        public async Task<Response<List<NominaDTO>>> Handle(GetNominasByCompanyIdAndPeriodCommand request, CancellationToken cancellationToken)
        {
            var nominas = await _repositoryAsync.ListAsync(new NominasByCompanyIdSpecification(request.CompanyId, request.PeriodoId));
            var copiaNominas = new List<NominaDTO>();
            Periodo periodo = await _repositoryAsyncPeriodo.GetByIdAsync(request.PeriodoId);
            if (nominas == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.CompanyId}");
            }
            else
            {
                foreach( var nomina in nominas )
                {
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
}
