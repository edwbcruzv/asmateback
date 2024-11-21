using Application.Interfaces;
using Application.Specifications.Users;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Usuarios.Users.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<Response<int>>
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public byte[]? UserPassword { get; set; }
        public string? UserProfile { get; set; }
        public string UserEmail { get; set; }
        public byte UserType { get; set; }
        public string UserPasswordAux { get; set; }
        public IFormFile File { get; set; }
        public int CompanyId { get; set; }

    }
    public class Handler : IRequestHandler<CreateUserCommand, Response<int>>
    {
        private readonly IRepositoryAsync<User> _repositoryAsyncUser;
        private readonly IMapper _mapper;
        private readonly IRsa _rsa;
        private readonly IFilesManagerService _filesManagerService;
        private readonly IRepositoryAsync<MenuUserSelector> _repositoryAsyncMenuUserSelector;
        private readonly IRepositoryAsync<SubMenuUserSelector> _repositoryAsyncSubMenuUserSelector;
        private readonly IRepositoryAsync<ContractsUserCompany> _repositoryAsyncContractsUserCompany;

        public Handler(IRepositoryAsync<User> repositoryAsyncUser, IMapper mapper, IRsa rsa, IFilesManagerService filesManagerService,
            IRepositoryAsync<MenuUserSelector> repositoryAsyncMenuUserSelector, IRepositoryAsync<SubMenuUserSelector> repositoryAsyncSubMenuUserSelector, 
            IRepositoryAsync<ContractsUserCompany> repositoryAsyncContractsUserCompany)
        {
            _repositoryAsyncUser = repositoryAsyncUser;
            _mapper = mapper;
            _rsa = rsa;
            _filesManagerService = filesManagerService;
            _repositoryAsyncMenuUserSelector = repositoryAsyncMenuUserSelector;
            _repositoryAsyncSubMenuUserSelector = repositoryAsyncSubMenuUserSelector;
            _repositoryAsyncContractsUserCompany = repositoryAsyncContractsUserCompany;
        }

        public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (await _repositoryAsyncUser.FirstOrDefaultAsync(new UserByNickNameSpecification(request.NickName)) != null)
            {
                throw new KeyNotFoundException($"Nickname {request.NickName} no disponible");
            }
            else
            {
                request.UserPassword = _rsa.Encript(request.UserPasswordAux);
                request.UserProfile = _filesManagerService.saveUserPhoto(request.File, request.NickName);

                var nuevoRegistro = _mapper.Map<User>(request);

                var data = await _repositoryAsyncUser.AddAsync(nuevoRegistro);

                //Creando Menu por defecto
                MenuUserSelector menu = new MenuUserSelector();
                menu.UserId = data.Id;
                menu.MenuId = 1006;

                await _repositoryAsyncMenuUserSelector.AddAsync(menu);

                menu = new MenuUserSelector();
                menu.UserId = data.Id;
                menu.MenuId = 1007;

                await _repositoryAsyncMenuUserSelector.AddAsync(menu);

                //Creando submenu por defecto
                //Mi portal
                SubMenuUserSelector subMenu = new SubMenuUserSelector();
                subMenu.UserId = data.Id;
                subMenu.MenuId = 1006;
                subMenu.SubMenuId = 1014;
                await _repositoryAsyncSubMenuUserSelector.AddAsync(subMenu);

                subMenu = new SubMenuUserSelector();
                subMenu.UserId = data.Id;
                subMenu.MenuId = 1006;
                subMenu.SubMenuId = 1020;
                await _repositoryAsyncSubMenuUserSelector.AddAsync(subMenu);

                subMenu = new SubMenuUserSelector();
                subMenu.UserId = data.Id;
                subMenu.MenuId = 1006;
                subMenu.SubMenuId = 1024;
                await _repositoryAsyncSubMenuUserSelector.AddAsync(subMenu);

                subMenu = new SubMenuUserSelector();
                subMenu.UserId = data.Id;
                subMenu.MenuId = 1006;
                subMenu.SubMenuId = 1025;
                await _repositoryAsyncSubMenuUserSelector.AddAsync(subMenu);

                //Centro de soporte
                subMenu = new SubMenuUserSelector();
                subMenu.UserId = data.Id;
                subMenu.MenuId = 1007;
                subMenu.SubMenuId = 1017;
                await _repositoryAsyncSubMenuUserSelector.AddAsync(subMenu);


                subMenu = new SubMenuUserSelector();
                subMenu.UserId = data.Id;
                subMenu.MenuId = 1007;
                subMenu.SubMenuId = 1018;
                await _repositoryAsyncSubMenuUserSelector.AddAsync(subMenu);

                subMenu = new SubMenuUserSelector();
                subMenu.UserId = data.Id;
                subMenu.MenuId = 1007;
                subMenu.SubMenuId = 1019;
                await _repositoryAsyncSubMenuUserSelector.AddAsync(subMenu);

                //Compania
                ContractsUserCompany company = new ContractsUserCompany();
                company.UserId = data.Id;
                company.CompanyId = request.CompanyId;

                await _repositoryAsyncContractsUserCompany.AddAsync(company);

                return new Response<int>(data.Id);
            }


        }
    }
}
