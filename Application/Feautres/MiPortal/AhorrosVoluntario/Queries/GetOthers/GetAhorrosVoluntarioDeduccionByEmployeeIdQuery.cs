using Application.DTOs.MiPortal.Ahorros;
using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosVoluntario;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosVoluntario.Queries.GetOthers
{
    public class GetAhorrosVoluntarioDeduccionByEmployeeIdQuery : IRequest<Response<double>>
    {

        public int Id { get; set; }

        public class Handler : IRequestHandler<GetAhorrosVoluntarioDeduccionByEmployeeIdQuery, Response<double>>
        {
           
            private readonly IAhorroVoluntarioService _serviceAhorroVoluntario;

            public Handler(IAhorroVoluntarioService serviceAhorroVoluntario)
            {
                _serviceAhorroVoluntario = serviceAhorroVoluntario;
            }

            public async Task<Response<double>> Handle(GetAhorrosVoluntarioDeduccionByEmployeeIdQuery request, CancellationToken cancellationToken)
            {
                double deduccion = await _serviceAhorroVoluntario.GetDeduccion(request.Id);

                return new Response<double>(deduccion);
            }
        }
    }
}
