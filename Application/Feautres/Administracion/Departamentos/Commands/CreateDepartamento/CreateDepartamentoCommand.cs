using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Departamentos.Commands.CreateDepartamento
{
    public class CreateDepartamentoCommand : IRequest<Response<int>>
    {
        public int CompanyId { get; set; }
        public string? Clave { get; set; }
        public string Descripcion { get; set; }

        public class Handler : IRequestHandler<CreateDepartamentoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IDepartamentoService _departamentoService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Departamento> repositoryAsyncDepartamento, IMapper mapper, IRepositoryAsync<Company> repositoryAsyncCompany, IDepartamentoService departamentoService)
            {
                _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
                _mapper = mapper;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _departamentoService = departamentoService;
            }

            public async Task<Response<int>> Handle(CreateDepartamentoCommand request, CancellationToken cancellationToken)
            {
                var company = await _repositoryAsyncCompany.GetByIdAsync(request.CompanyId);

                var elem = _mapper.Map<Departamento>(request);

                if (request.Clave == null) { elem.Clave = ""; }

                var data = await _repositoryAsyncDepartamento.AddAsync(elem);

                if (data.Clave == null || data.Clave.Length == 0)
                {
                    data.Clave = _departamentoService.GenerateClave(company.Name,data.Descripcion,data.Id);
                }

                await _repositoryAsyncDepartamento.UpdateAsync(data);

                return new Response<int>(data.Id);
            }

            


        }
    }
}
