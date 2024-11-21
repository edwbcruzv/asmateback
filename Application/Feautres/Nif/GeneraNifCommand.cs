using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Nif
{
    public class GeneraNifCommand : IRequest<Response<String>>
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public IFormFile File { get; set; }
    }
    public class Handler : IRequestHandler<GeneraNifCommand, Response<String>>
    {
        private readonly INifService _nifService;
        public Handler(INifService nifService)
        {
            _nifService = nifService;
        }
        public async Task<Response<String>> Handle(GeneraNifCommand request, CancellationToken cancellationToken)
        {
            
            return await _nifService.Nif(request.FechaInicio, request.FechaFin,request.File);
        }
    }
}
