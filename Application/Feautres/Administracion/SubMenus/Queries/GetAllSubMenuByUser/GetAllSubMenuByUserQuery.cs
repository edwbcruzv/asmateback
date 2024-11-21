using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications.SubMenu;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Administracion.SubMenus.Queries.GetAllSubMenuByUser
{
    public class GetAllSubMenuByUserQuery : IRequest<Response<List<SubMenuDto>>>
    {
        public int Id { set; get; }
        public class Handler : IRequestHandler<GetAllSubMenuByUserQuery, Response<List<SubMenuDto>>>
        {
            private readonly IRepositoryAsync<SubMenu> _repositoryAsyncSubMenu;
            private readonly IRepositoryAsync<SubMenuUserSelector> _repositoryAsyncSubMenuUserSelector;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<SubMenu> repositoryAsyncSubMenu, IMapper mapper, IRepositoryAsync<SubMenuUserSelector> repositoryAsyncSubMenuUserSelector)
            {
                _repositoryAsyncSubMenu = repositoryAsyncSubMenu;
                _mapper = mapper;
                _repositoryAsyncSubMenuUserSelector = repositoryAsyncSubMenuUserSelector;
            }

            public async Task<Response<List<SubMenuDto>>> Handle(GetAllSubMenuByUserQuery request, CancellationToken cancellationToken)
            {
                var userSubMenus = await _repositoryAsyncSubMenuUserSelector.ListAsync(new SubMenuUserSelectorByUserSpecification(request.Id));

                List<SubMenuDto> SubMenus = new List<SubMenuDto>();

                foreach (var item in userSubMenus)
                {
                    var SubMenu = await _repositoryAsyncSubMenu.GetByIdAsync(item.SubMenuId);

                    SubMenus.Add(_mapper.Map<SubMenuDto>(SubMenu));
                }

                return new Response<List<SubMenuDto>>(SubMenus);
            }
        }
    }
}
