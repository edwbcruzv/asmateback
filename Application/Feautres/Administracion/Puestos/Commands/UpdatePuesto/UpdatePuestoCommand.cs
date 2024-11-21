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

namespace Application.Feautres.Administracion.Puestos.Commands.UpdatePuesto
{
    public class UpdatePuestoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public int DepartamentoId { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }

        public class Handler : IRequestHandler<UpdatePuestoCommand, Response<int>>
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

            public async Task<Response<int>> Handle(UpdatePuestoCommand request, CancellationToken cancellationToken)
            {
                var puesto = await _repositoryAsyncPuesto.GetByIdAsync(request.Id);

                if (puesto == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        var departamento = await _repositoryAsyncDepartamento.GetByIdAsync(puesto.DepartamentoId);

                        puesto.DepartamentoId = request.DepartamentoId;
                        puesto.Clave = request.Clave;
                        puesto.Descripcion = request.Descripcion;

                        if (puesto.Clave == null || puesto.Clave.Length == 0)
                        {
                            puesto.Clave = _puestoService.GenerateClave(departamento.Descripcion, puesto.Descripcion, puesto.Id);
                        }

                        await _repositoryAsyncPuesto.UpdateAsync(puesto);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }

                    return new Response<int>(puesto.Id);
                }

            }
        }
    }
}
