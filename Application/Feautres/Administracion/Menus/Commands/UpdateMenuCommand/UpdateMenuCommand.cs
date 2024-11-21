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

namespace Application.Feautres.Administracion.Menus.Commands.UpdateMenuCommand
{
    public class UpdateMenuCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuSource { get; set; }
        public string MenuIcon { get; set; }
        public string MenuOrder { get; set; }

    }
    public class Handler : IRequestHandler<UpdateMenuCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Menu> _repositoryAsync;

        public Handler(IRepositoryAsync<Menu> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;

        }

        public async Task<Response<int>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var Menu = await _repositoryAsync.GetByIdAsync(request.Id);

            if (Menu == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                Menu.MenuName = request.MenuName;
                Menu.MenuSource = request.MenuSource;
                Menu.MenuIcon = request.MenuIcon;
                Menu.MenuOrder = request.MenuOrder;

                await _repositoryAsync.UpdateAsync(Menu);

                return new Response<int>(Menu.Id);



            }
        }
    }
}
