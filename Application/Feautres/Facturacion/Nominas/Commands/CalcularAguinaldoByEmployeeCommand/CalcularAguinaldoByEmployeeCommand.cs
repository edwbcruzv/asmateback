using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.Nominas.Commands.CalcularAguinaldoByEmployeeCommand
{
    public class CalcularAguinaldoByEmployeeCommand : IRequest<Response<double>>
    {
        public int EmployeeID { get; set; }
        public int PeriodoID { get; set; }  
    }
    public class Handler : IRequestHandler<CalcularAguinaldoByEmployeeCommand, Response<double>>
    {
        public INominaService _nominaService;
        public Handler(INominaService nominaService)
        {
            _nominaService = nominaService;
        }
        public Task<Response<double>> Handle(CalcularAguinaldoByEmployeeCommand request, CancellationToken cancellationToken)
        {
            return _nominaService.generateAguinaldoByEmployee(request.EmployeeID, request.PeriodoID);
        }
    }
}
