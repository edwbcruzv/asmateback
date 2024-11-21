using Application.DTOs.MiPortal.Ahorros;
using Application.Feautres.MiPortal.RetirosAhorroVoluntario.Queries.GetAllRetirosAhorroVoluntario;
using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosVoluntario;
using Application.Specifications.MiPortal.RetirosAhorroVoluntario;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Queries.GetRetiroAhorroVoluntarioByEmployeeId
{
    public class GetRetiroAhorroVoluntarioByEmployeeIdQuery : IRequest<Response<List<RetiroAhorroVoluntarioDTO>>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetRetiroAhorroVoluntarioByEmployeeIdQuery, Response<List<RetiroAhorroVoluntarioDTO>>>
        {
            private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryAsync;
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<RetiroAhorroVoluntario> repositoryAsync, IMapper mapper, IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
            }

            public async Task<Response<List<RetiroAhorroVoluntarioDTO>>> Handle(GetRetiroAhorroVoluntarioByEmployeeIdQuery request, CancellationToken cancellationToken)
            {
                List<RetiroAhorroVoluntarioDTO> retirosAhorro = new List<RetiroAhorroVoluntarioDTO>();

                var list = await _repositoryAsyncAhorroVoluntario.ListAsync(new AhorroVoluntarioByEmployeeIdSpecification(request.Id));

                foreach (var item in list)
                {
                    var retiros = await _repositoryAsync.ListAsync(new RetiroAhorroVoluntarioByAhorroVoluntarioIdSpecification(item.Id));
                    var retiros_dto = _mapper.Map<List<RetiroAhorroVoluntarioDTO>>(retiros);
                    retirosAhorro.AddRange(retiros_dto);
                }

                return new Response<List<RetiroAhorroVoluntarioDTO>>(retirosAhorro); ;
            }
        }
    }
}
