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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Feautres.Administracion.Departamentos.Commands.UpdateDepartamento
{
    public class UpdateDepartamentoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }


        public class Handler : IRequestHandler<UpdateDepartamentoCommand, Response<int>>
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

            public async Task<Response<int>> Handle(UpdateDepartamentoCommand request, CancellationToken cancellationToken)
            {
                var departamento = await _repositoryAsyncDepartamento.GetByIdAsync(request.Id);
                var company = await _repositoryAsyncCompany.GetByIdAsync(request.CompanyId);

                if (departamento == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        departamento.CompanyId = request.CompanyId;
                        departamento.Clave = request.Clave;
                        departamento.Descripcion = request.Descripcion;

                        if (departamento.Clave == null || departamento.Clave.Length == 0)
                        {
                            departamento.Clave = _departamentoService.GenerateClave(company.Name, departamento.Descripcion, departamento.Id);
                        }
                        await _repositoryAsyncDepartamento.UpdateAsync(departamento);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }

                    return new Response<int>(departamento.Id);
                }

            }
        }
    }
}
