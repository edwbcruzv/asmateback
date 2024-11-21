using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications.MenuUser;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Administracion.Menus.Queries.GetAllMenuByUser
{
    public class GetAllMenuByUserQuery : IRequest<Response<List<MenuDto>>>
    {
        public int Id { set; get; }
        public class Handler : IRequestHandler<GetAllMenuByUserQuery, Response<List<MenuDto>>>
        {
            private readonly IRepositoryAsync<Menu> _repositoryAsyncMenu;
            private readonly IRepositoryAsync<MenuUserSelector> _repositoryAsyncMenuUserSelector;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Menu> repositoryAsyncMenu, IMapper mapper, IRepositoryAsync<MenuUserSelector> repositoryAsyncMenuUserSelector)
            {
                _repositoryAsyncMenu = repositoryAsyncMenu;
                _mapper = mapper;
                _repositoryAsyncMenuUserSelector = repositoryAsyncMenuUserSelector;
            }

            public async Task<Response<List<MenuDto>>> Handle(GetAllMenuByUserQuery request, CancellationToken cancellationToken)
            {
                var userMenus = await _repositoryAsyncMenuUserSelector.ListAsync(new MenuUserSelectorByUserSpecification(request.Id));

                List<MenuDto> menus = new List<MenuDto>();

                foreach (var item in userMenus)
                {
                    var menu = await _repositoryAsyncMenu.GetByIdAsync(item.MenuId);

                    menus.Add(_mapper.Map<MenuDto>(menu));
                }

                return new Response<List<MenuDto>>(menus);
            }
        }
    }
}
