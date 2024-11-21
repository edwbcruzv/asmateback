using Application.DTOs.Administracion;
using Application.DTOs.MiPortal.Incidencias;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.Catalogos;
using Application.Specifications.Employees;
using Application.Specifications.MiPortal;
using Application.Wrappers;
using Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class RegistroAsistenciaServices : IRegistroAsistenciaServices
    {
        public IRepositoryAsync<TipoAsistencia> _repositoryTipoAsistencia { get; set; }
        public IRepositoryAsync<RegistroAsistencia> _repositoryRegistroAsistencia { get; set; }
        public IRepositoryAsync<Periodo> _repositoryPeriodo { get; set; }

        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

        private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencia;

        private readonly IRepositoryAsync<Vacacion> _repositoryAsyncVacacion;

        public RegistroAsistenciaServices(IRepositoryAsync<TipoAsistencia> repositoryTipoAsistencia,
            IRepositoryAsync<RegistroAsistencia> repositoryRegistroAsistencia,
            IRepositoryAsync<Periodo> repositoryPeriodo,
            IRepositoryAsync<Employee> repositoryAsyncEmployee,
            IRepositoryAsync<Incidencia> repositoryAsyncIncidencia,
            IRepositoryAsync<Vacacion> repositoryAsyncVacacion)
        {
            _repositoryTipoAsistencia = repositoryTipoAsistencia;
            _repositoryRegistroAsistencia = repositoryRegistroAsistencia;
            _repositoryPeriodo = repositoryPeriodo;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
            _repositoryAsyncIncidencia = repositoryAsyncIncidencia;
            _repositoryAsyncVacacion = repositoryAsyncVacacion;
            
        }

        public async Task<List<AsistenciaResumenDto>> getAsistenciaByPeriodo(int PeriodoId, int EmpleadoId)
        {

            List<AsistenciaResumenDto> listAsistencias = new List<AsistenciaResumenDto>();

            var tipoAsistencias = await _repositoryTipoAsistencia.ListAsync();
            var periodo = await _repositoryPeriodo.GetByIdAsync(PeriodoId);
            var controlAsistencias = 0;
            foreach(var temp in tipoAsistencias)
            {

                var tempRegistroAsistencia = new AsistenciaResumenDto();

                tempRegistroAsistencia.Clave = temp.Clave;
                tempRegistroAsistencia.Descripcion = temp.Descripcion;
                var asistencias = await _repositoryRegistroAsistencia.ListAsync(
                    new AsistenciaByEmployeeAndTipoAsistenciaAndDesdeAndHastaSpecification(EmpleadoId, temp.Id, periodo.Desde, periodo.Hasta)
                );
                if (asistencias.Count == 0)
                {
                    controlAsistencias += 1;
                }
                tempRegistroAsistencia.Cantidad = asistencias.Count;
                tempRegistroAsistencia.SePagaIncapacidad = temp.SePagaIncapacidad;
                tempRegistroAsistencia.SePagaPtu = temp.SePagaPtu;
                tempRegistroAsistencia.SePagaNomina = temp.SePagaNomina;
                tempRegistroAsistencia.SePagaAguinaldo = temp.SePagaAguinaldo;

                listAsistencias.Add(tempRegistroAsistencia);

            }
            if (controlAsistencias == tipoAsistencias.Count)
            {
                throw new ApiException($"No existen asistencias cargadas para el empleado con id {EmpleadoId} en el periodo desde {periodo.Desde.ToShortDateString()} hasta {periodo.Hasta.ToShortDateString()}.");
            }

            return listAsistencias;

        }

        public async Task<List<AsistenciaResumenDto>> getAsistenciaByRango(DateTime Desde, DateTime Hasta, int EmpleadoId)
        {
            List<AsistenciaResumenDto> listAsistencias = new List<AsistenciaResumenDto>();

            var tipoAsistencias = await _repositoryTipoAsistencia.ListAsync();
            

            foreach (var temp in tipoAsistencias)
            {

                var tempRegistroAsistencia = new AsistenciaResumenDto();

                tempRegistroAsistencia.Clave = temp.Clave;
                tempRegistroAsistencia.Descripcion = temp.Descripcion;
                var asistencias = await _repositoryRegistroAsistencia.ListAsync(
                    new AsistenciaByEmployeeAndTipoAsistenciaAndDesdeAndHastaSpecification(EmpleadoId, temp.Id, Desde, Hasta)
                );
                tempRegistroAsistencia.Cantidad = asistencias.Count;
                tempRegistroAsistencia.SePagaIncapacidad = temp.SePagaIncapacidad;
                tempRegistroAsistencia.SePagaPtu = temp.SePagaPtu;
                tempRegistroAsistencia.SePagaNomina = temp.SePagaNomina;
                tempRegistroAsistencia.SePagaAguinaldo = temp.SePagaAguinaldo;

                listAsistencias.Add(tempRegistroAsistencia);

            }
            return listAsistencias;
        }

        public async Task<DiasIncidenciaDto> getDiasIncidenciaByEmployee(int employeeId, int aniosTrabajados)
        {
            var employee = await _repositoryAsyncEmployee.GetByIdAsync(employeeId);
            
            int anio = DateTime.Now.Year;
            DateTime desde = new DateTime(anio, 01, 01);
            DateTime hasta = new DateTime(anio, 12, 31);

            var listaAsistencias = await getAsistenciaByRango(desde, hasta, employee.Id);
            var listaVacacionesPorEmployee = await _repositoryAsyncIncidencia.ListAsync(new IncidenciasByEmployeeIdAndTipoIdSpecification(employee.Id, 1));
            DiasIncidenciaDto diasIncidenciaDTO = new DiasIncidenciaDto();
            int diasVacacionesPorAnio = 0;
            if (aniosTrabajados == 0)
            {
                diasIncidenciaDTO.diasVacaciones = 0;
            }
            else
            {
                var vacacion = await _repositoryAsyncVacacion.FirstOrDefaultAsync(new VacacionByAnioSpecification(aniosTrabajados, employee.CompanyId));
                if (vacacion == null)
                {
                    throw new KeyNotFoundException($"No se encontro registro de vacaciones para {aniosTrabajados} años trabajados");
                }
                else
                {
                    diasVacacionesPorAnio += vacacion.Dias;
                }

            }

            foreach (Incidencia vacacion in listaVacacionesPorEmployee)
            {
                if (vacacion.EstatusId == 2)
                {
                    diasVacacionesPorAnio -= (int)vacacion.Dias;
                }

            }
            diasIncidenciaDTO.diasVacaciones = diasVacacionesPorAnio;
            foreach (var tipoAsistencia in listaAsistencias)
            {
                switch (tipoAsistencia.Clave)
                {
                    case "DA":
                        diasIncidenciaDTO.diasAdministrativos = tipoAsistencia.Cantidad;
                        break;
                    case "PT":
                        diasIncidenciaDTO.permisosSalirTemprano = tipoAsistencia.Cantidad;
                        break;
                    case "PL":
                        diasIncidenciaDTO.permisosLlegarTarde = tipoAsistencia.Cantidad;
                        break;
                    case "IN":
                        diasIncidenciaDTO.diasIncapacidad = tipoAsistencia.Cantidad;
                        break;
                    default:
                        break;
                }
            }
            return diasIncidenciaDTO;
            
        }

        public async Task<int> calcularDiasVacaciones(DateTime FechaInicio, DateTime FechaFin)
        {
            DateOnly fechaInicio = DateOnly.FromDateTime(FechaInicio);
            DateOnly fechaFin = DateOnly.FromDateTime(FechaFin);

            int anio = DateTime.Now.Year;
            DateOnly navidad = new DateOnly(anio, 12, 25);
            DateOnly anioNuevo = new DateOnly(anio + 1, 01, 01);
            DateOnly diaConstitucion = new DateOnly(anio, 02, 06);
            DateOnly natalicioBenito = new DateOnly(anio, 03, 20);
            DateOnly diaDelTrabajo = new DateOnly(anio, 05, 01);
            DateOnly diaIndependencia = new DateOnly(anio, 09, 16);
            DateOnly diaRevolucion = new DateOnly(anio, 11, 20);

            List<DateOnly> listaFechas = new List<DateOnly> { navidad, anioNuevo, diaConstitucion, natalicioBenito, diaDelTrabajo, diaIndependencia, diaRevolucion };

            // Falta agregar los dias festivos de la empresa con una tabla

            int diasEfectivos = 0;
            for (DateOnly fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
            {
                if ((int)fecha.DayOfWeek == 6 || (int)fecha.DayOfWeek == 0)
                {
                    diasEfectivos += 0;
                }
                else
                {
                    if (listaFechas.Contains(fecha))
                    {
                        diasEfectivos += 0;
                    }
                    else
                    {
                        diasEfectivos += 1;
                    }
                }
            }
            return diasEfectivos;
        }
    }
}
