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

namespace Application.Feautres.Usuarios.SubMenuUserSelectors.Commands.CreateSubMenuUserSelectorsCommand
{
    public class CreateSubMenuUserSelectorCommand : IRequest<Response<int>>
    {
        public int UserId { get; set; }
        public int SubMenuId { get; set; }
        public int menuId { get; set; }

    }
    public class Handler : IRequestHandler<CreateSubMenuUserSelectorCommand, Response<int>>
    {
        private readonly IRepositoryAsync<SubMenuUserSelector> _repositoryAsync;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<SubMenuUserSelector> repositoryAsync, IMapper mapper, IRsa rsa, IFilesManagerService filesManagerService)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;


        }

        public async Task<Response<int>> Handle(CreateSubMenuUserSelectorCommand request, CancellationToken cancellationToken)
        {

            var nuevoRegistro = _mapper.Map<SubMenuUserSelector>(request);

            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);


        }
    }
}
