using Application.DTOs.Kanban.Sistemas;
using Application.DTOs.Kanban.SistemasDepartamentos;
using Application.Interfaces;
using Application.Specifications.Kanban.SistemasDepartamentos;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.SistemasDepartamentos.Queries
{
    public class GetAllSistemasDepartamentosByDepartamentoIdQuery : IRequest<Response<List<SistemaDTO>>>
    {

        public int DepartamentoId { get; set; }

        public class Handler : IRequestHandler<GetAllSistemasDepartamentosByDepartamentoIdQuery, Response<List<SistemaDTO>>>
        {
            private readonly IRepositoryAsync<SistemaDepartamento> _repositoryAsync;
            private readonly IRepositoryAsync<Sistema> _repositoryAsyncSistema;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<SistemaDepartamento> repositoryAsync, IRepositoryAsync<Sistema> repositoryAsyncSistema, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _repositoryAsyncSistema = repositoryAsyncSistema;
                _mapper = mapper;
            }

            public async Task<Response<List<SistemaDTO>>> Handle(GetAllSistemasDepartamentosByDepartamentoIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new SistemaDepartamentoByDepartamentoIdSpecification(request.DepartamentoId));

                List<Sistema> sistemas = new List<Sistema>();
                foreach (var item in list)
                {
                    sistemas.Add(await _repositoryAsyncSistema.GetByIdAsync(item.SistemaId));
                }

                var list_dto = _mapper.Map<List<SistemaDTO>>(sistemas);

                return new Response<List<SistemaDTO>>(list_dto);
            }
        }
    }
}
