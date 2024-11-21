using Application.Interfaces;
using Application.Specifications.Employees;
using Application.Wrappers;
using Domain.Entities;
using Application.Exceptions;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Specifications.Catalogos;
using Humanizer;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Application.DTOs.NIF;
using MimeKit.Encodings;

namespace Shared.Services
{
    public class NifService : INifService
    {
        private readonly IRepositoryAsync<Employee> _repositoryEmployee;
        private readonly IRepositoryAsync<Company> _repositoryCompany;
        private readonly IRepositoryAsync<Nif> _repositoryNif;
        private readonly IRepositoryAsync<NifResultado> _repositoryNifResultado;
        private readonly IRepositoryAsync<Uma> _repositoryUma;
        private readonly IRepositoryAsync<Vacacion> _repositoryVacacion;
        private readonly INominaService _nominaService;
        private readonly IExcelService _excelService;

        public NifService(IRepositoryAsync<Employee> repositoryEmployee, IRepositoryAsync<Company> repositoryCompany, IRepositoryAsync<Nif> repositoryNif, IRepositoryAsync<NifResultado> repositoryNifResultado, INominaService nominaService, IRepositoryAsync<Uma> repositoryUma, IRepositoryAsync<Vacacion> repositoryVacacion, IExcelService excelService)
        {
            _repositoryEmployee = repositoryEmployee;
            _repositoryCompany = repositoryCompany;
            _repositoryNif = repositoryNif;
            _repositoryNifResultado = repositoryNifResultado;
            _nominaService = nominaService;
            _repositoryUma = repositoryUma;
            _repositoryVacacion = repositoryVacacion;
            _excelService = excelService;
        }
        public async Task<Response<String>> Nif(DateTime fechaInicio, DateTime fechaFin, IFormFile file)
        {
            

            var listaEmployees = await _excelService.LeerExcelNif(file);

            if (listaEmployees == null) throw new ApiException("Error en la lectura del archivo");

            List<List<NifResultadoDTO>> listaNifsEmpleados = new List<List<NifResultadoDTO>>();

            Nif nif = new Nif();
            nif.NombreEjercicio = "Prueba";
            nif.FechaInicio = fechaInicio;
            nif.FechaFin = fechaFin;

            foreach (var employee in listaEmployees)
            { 
               
                var listaMesesPorEmployee = await CalcularNif(fechaInicio, fechaFin, employee, nif);
                listaNifsEmpleados.Add(listaMesesPorEmployee);
                
            }
            String rutaArchivo = await _excelService.EscribirResultadoNif(listaNifsEmpleados);
            Response<String> respuesta = new Response<String>();
            respuesta.Succeeded = true;
            respuesta.Message = null;
            respuesta.Data = rutaArchivo;
            return respuesta;
        }
        
        public async Task<List<NifResultadoDTO>> CalcularNif(DateTime fechaInicio, DateTime fechaFin, NifResultadoDTO employee, Nif nif)
        { 
            // Para cada mes dentro del intervalo de tiempo, se hacen todos los cálculos necesarios
            int ultimoMes = fechaFin.Month;
            double salarioDiario = employee.SueldoDiario;
            double salarioMensual = employee.SueldoBase;
            DateTime fechaContrato = employee.FechaIngreso;
            int year = fechaFin.Year;
            List<NifResultadoDTO> listaMesesPorEmployee = new List<NifResultadoDTO>();

            for (int primerMes = fechaInicio.Month; primerMes<=ultimoMes; primerMes++)
            {
                NifResultado nifResultado = new NifResultado();
                NifResultadoDTO nifResultadoPorMes = new NifResultadoDTO();
                nifResultado.NifId = nif.Id;

                nifResultado.IdEmpleado = employee.IdEmpleado;
                nifResultadoPorMes.IdEmpleado = employee.IdEmpleado;

                nifResultado.Nombre = employee.ApellidoPaterno + employee.ApellidoMaterno + employee.Nombre;
                
                nifResultadoPorMes.Nombre = employee.Nombre;
                nifResultadoPorMes.ApellidoPaterno = employee.ApellidoPaterno;
                nifResultadoPorMes.ApellidoMaterno = employee.ApellidoMaterno;


                nifResultado.Rfc = employee.Rfc;
                nifResultadoPorMes.Rfc = employee.Rfc;

                nifResultado.SueldoDiario = salarioDiario;
                nifResultadoPorMes.SueldoDiario = salarioDiario;

                nifResultado.SueldoBase = salarioMensual;
                nifResultadoPorMes.SueldoBase = salarioMensual;

                nifResultadoPorMes.FechaIngreso = employee.FechaIngreso;
                nifResultadoPorMes.diasVacciones = employee.diasVacciones;
                nifResultadoPorMes.diasAguinaldo = employee.diasAguinaldo;
                nifResultadoPorMes.PorcentajePrimaVacacional = employee.PorcentajePrimaVacacional;


                // Se calcula ISR/subsidio al empleo

                /*
                   * Calcular ISR - Subsidio al empledo
                   * 
                   * Si resultado < 0 -> Subsidio
                   * Si resultado > 0 -> Isr
                   * Si resultado = 0 -> Nada
                   * 
                */
                double isrSubsidio = await _nominaService.CalcularIsr(salarioMensual, year, 2) - await _nominaService.CalcularSubsidio(salarioMensual, year, 2);

                double subsidio = 0.0;
                double isr = 0.0;
                if (isrSubsidio > 0) isr = isrSubsidio;
                else if (isrSubsidio < 0) subsidio = (isrSubsidio * -1);

                
                nifResultado.Isr = isr;
                nifResultadoPorMes.Isr = isr;

                nifResultado.Subsidio = subsidio;
                nifResultadoPorMes.Subsidio = subsidio;
                   

                // Se calculan las cuotas obrero patronales, 

                double cuotaPatronalMonto;
                if (employee.SBC == 0) cuotaPatronalMonto = await _nominaService.CalcularCOP(0.0,salarioDiario, 30);
                else cuotaPatronalMonto = await _nominaService.CalcularCOP(employee.SBC,salarioDiario, 30);
                
                nifResultado.CuotasPatronales = cuotaPatronalMonto;
                nifResultadoPorMes.CuotasPatronales = cuotaPatronalMonto;

                // Se calcula la prima de antiguedad
                double primaAntiguedad = _nominaService.CalculoPrimaAntiguedad(salarioDiario, fechaContrato, fechaFin);
                
                nifResultado.PrimaAntiguedad = primaAntiguedad;
                nifResultadoPorMes.PrimaAntiguedad = primaAntiguedad;

                // Se calcula crédito infonavit
                double infonavit = _nominaService.CalculoInfonavit(salarioDiario);
                
                nifResultado.Infonavit = infonavit;
                nifResultadoPorMes.Infonavit = infonavit;

                // Se calcula el rcv
                double rcv = _nominaService.CalculoRCV(salarioDiario);
                
                nifResultado.Rcv = rcv;
                nifResultadoPorMes.Rcv = rcv;


                double primaVacacional = 0.0;
                double aguinaldo = 0.0;
                double montoVacaciones = 0.0;
                
                // Calculo de aguinaldo, primaVacacional y monto por vacaciones si es fin de año
                if (primerMes == 12)
                {
                    primaVacacional = await _nominaService.CalcularPrimaVacacional(fechaContrato, fechaInicio, fechaFin,
                       employee.diasVacciones, salarioDiario, employee.PorcentajePrimaVacacional);
                    aguinaldo = _nominaService.CalculoAguinaldo(salarioDiario, employee.diasAguinaldo, 365);
                    montoVacaciones = _nominaService.CalculoVacaciones(salarioDiario, employee.diasVacciones);
                }

                nifResultado.Aguinaldo = aguinaldo;
                nifResultadoPorMes.Aguinaldo = aguinaldo;

                nifResultado.PrimaVacacional = primaVacacional;
                nifResultadoPorMes.PrimaVacacional = primaVacacional;

                nifResultado.Vacaciones = montoVacaciones;
                nifResultadoPorMes.Vacaciones = montoVacaciones;

                // Se asigna el campo de mes y año al nifResultado
                nifResultado.Mes = primerMes;
                nifResultadoPorMes.Mes = primerMes;

                nifResultado.Año = year;
                nifResultadoPorMes.Año = year;
                // await _repositoryNifResultado.AddAsync(nifResultado);
                listaMesesPorEmployee.Add(nifResultadoPorMes);
            }

            // await _repositoryNif.AddAsync(nif);
            return listaMesesPorEmployee;
        }
    }
}
