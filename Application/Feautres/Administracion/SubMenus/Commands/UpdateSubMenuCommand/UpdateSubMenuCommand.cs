using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.SubMenus.Commands.UpdateSubMenuCommand
{
    public class UpdateSubMenuCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuSource { get; set; }
        public string SubMenuIcon { get; set; }
        public int SubMenuOrder { get; set; }
        public int MenuId { get; set; }

    }
    public class Handler : IRequestHandler<UpdateSubMenuCommand, Response<int>>
    {
        private readonly IRepositoryAsync<SubMenu> _repositoryAsync;

        public Handler(IRepositoryAsync<SubMenu> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;

        }

        public async Task<Response<int>> Handle(UpdateSubMenuCommand request, CancellationToken cancellationToken)
        {
            var SubMenu = await _repositoryAsync.GetByIdAsync(request.Id);

            if (SubMenu == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                SubMenu.SubMenuName = request.SubMenuName;
                SubMenu.SubMenuSource = request.SubMenuSource;
                SubMenu.SubMenuIcon = request.SubMenuIcon;
                SubMenu.SubMenuOrder = request.SubMenuOrder;

                await _repositoryAsync.UpdateAsync(SubMenu);

                return new Response<int>(SubMenu.Id);


            }
        }
    }
}
