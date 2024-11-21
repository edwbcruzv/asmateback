using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosWise.Commands.CreateAhorroWiseCommand
{
    public class CreateAhorroWiseCommand : IRequest<Response<int>>
    {
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        //public DateTime FechaFinal { get; set; }
        public int PeriodoInicial { get; set; }
        //public int PeriodoFinal { get; set; }
        public EstatusOperacion Estatus { get; set; }
        //public float Rendimiento { get; set; }
        //public IFormFile FileConstancia { get; set; }
        //public IFormFile FilePago { get; set; }

        public class Handler : IRequestHandler<CreateAhorroWiseCommand, Response<int>>
        {
            private readonly IRepositoryAsync<AhorroWise> _repositoryAsyncAhorroWise;
            private readonly IFilesManagerService _filesManagerService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<AhorroWise> repositoryAsyncAhorroWise, IMapper mapper, IFilesManagerService filesManagerService)
            {
                _repositoryAsyncAhorroWise = repositoryAsyncAhorroWise;
                _mapper = mapper;
                _filesManagerService = filesManagerService;
            }

            public async Task<Response<int>> Handle(CreateAhorroWiseCommand request, CancellationToken cancellationToken)
            {
                var elem = _mapper.Map<AhorroWise>(request);

                //elem.SrcFileConstancia = _filesManagerService.saveAhorroWiseConstanciaPDF(request.FileConstancia, (elem.EmployeeId*10000)+elem.CompanyId);
                //elem.SrcFilePago = _filesManagerService.saveAhorroWisePagoPDF(request.FilePago, elem.Id);

                var data = await _repositoryAsyncAhorroWise.AddAsync(elem);
                return new Response<int>(data.Id);
            }
        }
    }
}
