
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Viaticos.Commands.CreateViaticoCommand
{
    public class CreateViaticoCommand : IRequest<Response<int>>
    {
        public int EmployeeId { get; set; }
        //public DateTime Fecha { get; set; }
        public int CompanyId { get; set; }
        //public string Estatus { get; set; }
        //public float Monto { get; set; }
        public String Descripcion { get; set; }
        //public int? BancoId { get; set; }
        //public string? NoCuenta { get; set; }
        //public IFormFile? PDF { get; set; }
        //public int? EmployeePagoId { get; set; }
        //public DateTime? FechaPago { get; set; }
        public float? MontoRecibido { get; set; }
        //public TipoViatico Tipo { get; set; }
        public int? EstadoId { get; set; }

        public class Handler : IRequestHandler<CreateViaticoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Viatico> repositoryAsyncViatico, IMapper mapper)
            {
                _repositoryAsyncViatico = repositoryAsyncViatico;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateViaticoCommand request, CancellationToken cancellationToken)
            {
                var viatico = _mapper.Map<Viatico>(request);
                viatico.Fecha = DateTime.Now;
                viatico.Estatus = EstatusViatico.Abierto;

                var data = await _repositoryAsyncViatico.AddAsync(viatico);
                return new Response<int>(data.Id);
            }
        }
    }
}
