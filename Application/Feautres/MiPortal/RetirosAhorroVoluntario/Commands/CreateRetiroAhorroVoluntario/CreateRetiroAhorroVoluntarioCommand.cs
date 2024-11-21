using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.CreateRetiroAhorroVoluntario
{
    public class CreateRetiroAhorroVoluntarioCommand : IRequest<Response<int>>
    {
        public int AhorroVoluntarioId { get; set; }
        public double Cantidad { get; set; }
        public double Porcentaje { get; set; }
        public bool SeguirAhorrando { get; set; }
        public IFormFile FileSolicitudFirmado { get; set; }

        public class Handler : IRequestHandler<CreateRetiroAhorroVoluntarioCommand, Response<int>>
        {
            private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryAsyncRetiroAhorroVoluntario;
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
            private readonly IRetiroAhorroVoluntarioService _retiroAhorarioService;
            private readonly IMapper _mapper;

            public Handler(IMapper mapper, IRepositoryAsync<RetiroAhorroVoluntario> repositoryAsyncRetiroAhorroVoluntario, IRetiroAhorroVoluntarioService retiroAhorarioService, IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario)
            {
                _mapper = mapper;
                _repositoryAsyncRetiroAhorroVoluntario = repositoryAsyncRetiroAhorroVoluntario;
                _retiroAhorarioService = retiroAhorarioService;
                _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
            }

            public async Task<Response<int>> Handle(CreateRetiroAhorroVoluntarioCommand request, CancellationToken cancellationToken)
            {
                var elem = _mapper.Map<RetiroAhorroVoluntario>(request);
                elem.Estatus = EstatusRetiro.Pendiente;

                var data = await _repositoryAsyncRetiroAhorroVoluntario.AddAsync(elem);
                data.SrcDocSolicitudFirmado = _retiroAhorarioService.SaveSolicitudFirmada(request.FileSolicitudFirmado, data.Id, request.AhorroVoluntarioId);

                AhorroVoluntario ahorro_voluntario = await _repositoryAsyncAhorroVoluntario.GetByIdAsync(data.AhorroVoluntarioId);

                await _repositoryAsyncRetiroAhorroVoluntario.UpdateAsync(data);
                await _retiroAhorarioService.EnviarCorreoEstatus(data, ahorro_voluntario);
                return new Response<int>(data.Id);
            }
        }

    }
}
