using Application.DTOs.Administracion;
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

namespace Application.Feautres.Catalogos.UnidadMedidas.Commands.UpdateUnidadMedidasCommand
{

    public class UpdateUnidadMedidasCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public bool Estatus { get; set; }
    }

    public class UpdateUnidadMedidasCommandHandler : IRequestHandler<UpdateUnidadMedidasCommand, Response<int>>
    {
        private readonly IRepositoryAsync<UnidadMedida> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateUnidadMedidasCommandHandler(IRepositoryAsync<UnidadMedida> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<int>> Handle(UpdateUnidadMedidasCommand request, CancellationToken cancellationToken)
        {
            var unidadMedida = await _repositoryAsync.GetByIdAsync(request.Id);


            if (unidadMedida == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                unidadMedida.Estatus = request.Estatus;

                await _repositoryAsync.UpdateAsync(unidadMedida);

                return new Response<int>(unidadMedida.Id);
            }
        }
    }
}
