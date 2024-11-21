using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Catalogos.CveProductos.Commands.UpdateCveProductosCommand
{
    public class UpdateCveProductosCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public bool? Estatus { get; set; }

    }

    public class UpdateCveProductosCommandHandler : IRequestHandler<UpdateCveProductosCommand, Response<int>>
    {
        private readonly IRepositoryAsync<CveProducto> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateCveProductosCommandHandler(IRepositoryAsync<CveProducto> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<int>> Handle(UpdateCveProductosCommand request, CancellationToken cancellationToken)
        {
            var cveProducto = await _repositoryAsync.GetByIdAsync(request.Id);


            if (cveProducto == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                cveProducto.Estatus = request.Estatus;

                await _repositoryAsync.UpdateAsync(cveProducto);

                return new Response<int>(cveProducto.Id);
            }
        }
    }
}
