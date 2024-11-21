using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.SubMenus.Queries.GetAllSubMenu
{
    public class GetAllSubMenuQuery : IRequest<Response<List<SubMenuDto>>>
    {
        public class Handler : IRequestHandler<GetAllSubMenuQuery, Response<List<SubMenuDto>>>
        {
            private readonly IRepositoryAsync<SubMenu> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<SubMenu> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<SubMenuDto>>> Handle(GetAllSubMenuQuery request, CancellationToken cancellationToken)
            {
                var SubMenus = await _repositoryAsync.ListAsync();

                var SubMenusDto = _mapper.Map<List<SubMenuDto>>(SubMenus);

                return new Response<List<SubMenuDto>>(SubMenusDto);
            }
        }
    }
}
