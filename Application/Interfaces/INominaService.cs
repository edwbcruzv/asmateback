using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications.Catalogos;
using Application.Specifications.Employees;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INominaService
    {
        #region generadores
        public Task<Response<bool>> generateNominaByPeriodo(int PeriodoId);
        public Task<Response<bool>> generateAguinaldoByPeriodo(int PeriodoId);
        public Task<Response<double>> generateAguinaldoByEmployee(int EmployeeID, int periodoID);
        #endregion

        #region beneficios
        public Task<double> CalcularPrimaVacacional(DateTime fechaIngreso, DateTime desdeNomina, DateTime hastaNomina, int diasVacaciones, double SueldoDiario, double porcentajePrimaVacacional);
        public double CalcularIncapacidad(string tipoIncapacidad, int diasIncapacidad, double diasAsistecia, double salarioDiario, double CNSalarioDiario);
        public Task<double> CalcularIsr(double monto, int anio, int tipo);
        public Task<double> CalcularSubsidio(double monto, int anio, int tipo);
        public Task<double> CalcularCOP(double sbcEmpleado, double sueldoDiario, double asistencias);
        public double CalculoAguinaldo(double salarioDiario, int diasAguinaldo, int asistencias);
        public double CalculoRCV(double salarioDiario);
        public double CalculoInfonavit(double salarioDiario);
        public double CalculoPrimaAntiguedad(double salarioDiario, DateTime fechaContrato, DateTime fechaFin);
        public double CalculoVacaciones(double salarioDiario, int diasVacaciones);
        public int anioEntreDosFechas(DateTime fechaInicio, DateTime fechaFin);
        #endregion

        #region funciones adicionales
        public Task<double> CalcularTotal(Nomina nomina);
        #endregion

    }


}


