﻿using Application.Interfaces;
using Application.Specifications.Employees;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Administracion.Employees.Commands.CreateEmployeeCommand
{
    public class CreateEmployeeCommand : IRequest<Response<int>>
    {
        public int CompanyId { get; set; }
        public Int64 NoEmpleado { get; set; }
        public string? Rfc { get; set; }
        public string? Nss { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Curp { get; set; }
        public int? EstadoCivil { get; set; }
        public string? Mail { get; set; }
        public string? NoCuenta { get; set; }
        public string? CLABE { get; set; }
        public int? BancoId { get; set; }
        public int? UserId { get; set; }
        public string MailCorporativo { get; set; }
        public string TelefonoMovil { get; set; }
        public string? TelefonoFijo { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Calle { get; set; }
        public string? NoInt { get; set; }
        public string? NoExt { get; set; }
        public string? Estado { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime? FechaContrato { get; set; }
        public string? RelojChecador { get; set; }
        public string? RegistroPatronal { get; set; }
        public int? PuestoId { get; set; }
        public DateTime? FinContrato { get; set; }
        public int? TipoNomina { get; set; }
        public double? SalarioMensual { get; set; }
        public double? SalarioDiario { get; set; }
        public double? SalarioDiarioIntegrado { get; set; }
        public double? SBC { get; set; }
        public double? Porcentaje { get; set; }
        public int? TipoContratoId { get; set; }
        public int? TipoJornadaId { get; set; }
        public int? TipoPrevicionSocial { get; set; }
        public int? FormaPagoId { get; set; }
        public int? TipoPeriocidadPagoId { get; set; }
        public int? TipoRegimenId { get; set; }
        public int? TipoRiesgoTrabajoId { get; set; }
        public int? TipoIncapacidadId { get; set; }
        public string? CreditoFonacot { get; set; }
        public string? CreditoInfonavit { get; set; }
        public double? DescuentoCreditoHipo { get; set; }
        public int? RegimenFiscalId { get; set; }
        public short? CuentaIndividual { get; set; }
        public double? FondoAhorroEmpleado { get; set; }
        public double? FondoAhorroEmpresa { get; set; }
        public double? PorcentajePrimaVacacional { get; set; }
        public double? AjusteIsr { get; set; }
        public int? JefeId { get; set; }

    }
    public class Handler : IRequestHandler<CreateEmployeeCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;
        private readonly IMapper _mapper;


        public Handler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public IRepositoryAsync<Employee> RepositoryAsync => _repositoryAsync;

        public async Task<Response<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employee = await _repositoryAsync.FirstOrDefaultAsync(new EmployeeByNoEmpleadoSpecification(request.NoEmpleado));

            if (employee != null)
            {
                throw new KeyNotFoundException($"El no. de empleado {request.NoEmpleado} ya está registrado.");
            }

            var nuevoRegistro = _mapper.Map<Employee>(request);
            nuevoRegistro.Estatus = 1;

            var data = await RepositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);


        }
    }
}
