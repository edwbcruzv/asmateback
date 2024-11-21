using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.SubMenus.Commands.CreateSubMenuCommand
{
    public class CreateSubMenuCommand : IRequest<Response<int>>
    {
        public string SubMenuName { get; set; }
        public string SubMenuSource { get; set; }
        public string SubMenuIcon { get; set; }
        public int SubMenuOrder { get; set; }
        public int MenuId { get; set; }

    }
    public class Handler : IRequestHandler<CreateSubMenuCommand, Response<int>>
    {
        private readonly IRepositoryAsync<SubMenu> _repositoryAsync;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<SubMenu> repositoryAsync, IMapper mapper, IRsa rsa, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;


        }

        public async Task<Response<int>> Handle(CreateSubMenuCommand request, CancellationToken cancellationToken)
        {

            var nuevoRegistro = _mapper.Map<SubMenu>(request);

            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);


        }
    }
}
