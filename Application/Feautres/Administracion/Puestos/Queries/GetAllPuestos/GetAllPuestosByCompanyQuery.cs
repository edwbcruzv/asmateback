using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications.Administracion;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Puestos.Queries.GetAllPuestos
{
    public class GetAllPuestosByCompanyQuery : IRequest<Response<List<PuestoDto>>>
    {
        public int CompanyId { get; set; }
        public class Handler : IRequestHandler<GetAllPuestosByCompanyQuery, Response<List<PuestoDto>>>
        {
            private readonly IRepositoryAsync<Puesto> _repositoryAsyncPuesto;
            private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Puesto> repositoryAsyncPuesto, IMapper mapper, IRepositoryAsync<Departamento> repositoryAsyncDepartamento)
            {
                _repositoryAsyncPuesto = repositoryAsyncPuesto;
                _mapper = mapper;
                _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
            }

            public async Task<Response<List<PuestoDto>>> Handle(GetAllPuestosByCompanyQuery request, CancellationToken cancellationToken)
            {
                List<Departamento> list_departamentos = await _repositoryAsyncDepartamento.ListAsync(new DepartamentoByCompanyIdSpecification(request.CompanyId));

                List<Puesto> puestos = new List<Puesto>();
                foreach (Departamento item in list_departamentos)
                {
                    var list_aux = await _repositoryAsyncPuesto.ListAsync(new PuestoByDepartamentoSpecification(item.Id), cancellationToken);
                    puestos.AddRange(list_aux);
                }

                Dictionary<int, string> dicc_departamentos = list_departamentos.ToDictionary(c => c.Id, c => c.Descripcion);

                var list_dto = new List<PuestoDto>();

                foreach (var elem in puestos)
                {
                    var dto = _mapper.Map<PuestoDto>(elem);
                    dto.Departamento = dicc_departamentos[elem.DepartamentoId];
                    list_dto.Add(dto);
                }

                return new Response<List<PuestoDto>>(list_dto);

            }
        }
    }
}
