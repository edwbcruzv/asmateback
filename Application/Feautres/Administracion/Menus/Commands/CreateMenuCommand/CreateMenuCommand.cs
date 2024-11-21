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

namespace Application.Feautres.Administracion.Menus.Commands.CreateMenuCommand
{
    public class CreateMenuCommand : IRequest<Response<int>>
    {
        public string MenuName { get; set; }
        public string MenuSource { get; set; }
        public string MenuIcon { get; set; }
        public string MenuOrder { get; set; }

    }
    public class Handler : IRequestHandler<CreateMenuCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Menu> _repositoryAsync;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<Menu> repositoryAsync, IMapper mapper, IRsa rsa, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;


        }

        public async Task<Response<int>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {

            var nuevoRegistro = _mapper.Map<Menu>(request);

            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);


        }
    }
}
