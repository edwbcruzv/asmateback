using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosVoluntario.Commands.CreateAhorroVoluntarioCommand
{
    public class CreateAhorroVoluntarioCommand : IRequest<Response<int>>
    {
        //llllllll
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        //public int PeriodoInicial { get; set; }
        //public int PeriodoFinal { get; set; }
        //public EstatusOperacion Estatus { get; set; }
        //public float Rendimiento { get; set; }
        public float Descuento { get; set; }

        public class Handler : IRequestHandler<CreateAhorroVoluntarioCommand, Response<int>>
        {
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
            private readonly IMapper _mapper;
            private readonly IAhorroVoluntarioService _ahorroVoluntarioService;

            public Handler(IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario, IMapper mapper, IAhorroVoluntarioService ahorroVoluntarioService)
            {
                _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
                _mapper = mapper;
                _ahorroVoluntarioService = ahorroVoluntarioService;
            }

            public async Task<Response<int>> Handle(CreateAhorroVoluntarioCommand request, CancellationToken cancellationToken)
            {
                var elem = _mapper.Map<AhorroVoluntario>(request);
                elem.Estatus = EstatusOperacion.Pendiente;

                var data = await _repositoryAsyncAhorroVoluntario.AddAsync(elem);
                await _ahorroVoluntarioService.EnviarCorreoEstatus(data);
                return new Response<int>(data.Id);
            }
        }
    }
}
