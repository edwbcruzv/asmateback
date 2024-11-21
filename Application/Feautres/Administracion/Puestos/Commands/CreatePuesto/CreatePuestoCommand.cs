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

namespace Application.Feautres.Administracion.Puestos.Commands.CreatePuesto
{
    public class CreatePuestoCommand : IRequest<Response<int>>
    {
        public int DepartamentoId { get; set; }
        public string? Clave { get; set; }
        public string Descripcion { get; set; }

        public class Handler : IRequestHandler<CreatePuestoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Puesto> _repositoryAsyncPuesto;
            private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
            private readonly IPuestoService _puestoService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Puesto> repositoryAsyncPuesto, IMapper mapper, IRepositoryAsync<Departamento> repositoryAsyncDepartamento, IPuestoService puestoService)
            {
                _repositoryAsyncPuesto = repositoryAsyncPuesto;
                _mapper = mapper;
                _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
                _puestoService = puestoService;
            }

            public async Task<Response<int>> Handle(CreatePuestoCommand request, CancellationToken cancellationToken)
            {
                var departamento = await _repositoryAsyncDepartamento.GetByIdAsync(request.DepartamentoId);
                var elem = _mapper.Map<Puesto>(request);
                
                if (request.Clave == null) { elem.Clave = ""; }

                var data = await _repositoryAsyncPuesto.AddAsync(elem);

                if (data.Clave == null || data.Clave.Length == 0)
                {
                    data.Clave = _puestoService.GenerateClave(departamento.Descripcion,data.Descripcion,data.Id);
                }

                await _repositoryAsyncPuesto.UpdateAsync(data);

                return new Response<int>(data.Id);
            }
        }
    }
}
