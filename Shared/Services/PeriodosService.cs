using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class PeriodosService : IPeriodosService
    {
        private readonly IRepositoryAsync<Periodo> _repositoryAsync;
        private readonly IRepositoryAsync<TipoPeriocidadPago> _repositoryAsyncTipoPeriocidadPago;
        public PeriodosService(IRepositoryAsync<Periodo> repositoryAsync, IRepositoryAsync<TipoPeriocidadPago> repositoryAsyncTipoPeriocidadPago)
        {
            _repositoryAsync = repositoryAsync;
            _repositoryAsyncTipoPeriocidadPago = repositoryAsyncTipoPeriocidadPago;
        }

        public async Task<Response<bool>> generaPeriodos(int Anio, int CompanyId, int TipoPeriocidadPagoId)
        {

            var itemPeriocidad = await _repositoryAsyncTipoPeriocidadPago.GetByIdAsync(TipoPeriocidadPagoId);



            var item = await _repositoryAsync.FirstOrDefaultAsync(new PeriodosByAnioAndTipoAndCompanySpecification(CompanyId, Anio, TipoPeriocidadPagoId));
            
            if (item != null)
            {
                throw new ApiException($"Periodos para CompañiaId {CompanyId} Año {Anio} Periocidad {TipoPeriocidadPagoId} previamente generados");

            }

            switch (itemPeriocidad.Clave)
            {
                case "01":
                    await CreateDiario(Anio, CompanyId, TipoPeriocidadPagoId);
                    break;
                case "02":
                    await CreateSemanal(Anio, CompanyId, TipoPeriocidadPagoId);
                    break;
                case "03":
                    await CreateCatorcena(Anio, CompanyId, TipoPeriocidadPagoId);
                    break;
                case "04":
                    await CreateQuincenas(Anio, CompanyId, TipoPeriocidadPagoId);
                    break;
                case "05":
                    await CreateMensual(Anio, CompanyId, TipoPeriocidadPagoId);
                    break;
                case "06":
                    await CreateBimestral(Anio, CompanyId, TipoPeriocidadPagoId);
                    break;
                case "10":
                    await CreateDecenal(Anio, CompanyId, TipoPeriocidadPagoId);
                    break;
                default:
                    throw new ApiException($"TipoPeriocidadPagoId {itemPeriocidad.Clave} {itemPeriocidad.Descripcion.TrimEnd()} no programado");

            }

            return new Response<bool>(true);
        
        }

        private async Task<Response<int>> CreateDiario(int Anio, int CompanyId, int TipoPeriocidadPagoId)
        {
            int dia = 1;
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 1; j <= DateTime.DaysInMonth(Anio, i); j++)
                {

                    var temp = new Periodo();
                    
                    DateTime inicioDia = new DateTime(Anio, i, j); // Hora de inicio (00:00:00)
                    DateTime finDia = inicioDia.AddDays(1).AddSeconds(-1); // Hora de fin (23:59:59)

                    temp.Desde = inicioDia;
                    temp.Hasta = finDia;
                    temp.Etapa = (Anio * 1000) + dia++;
                    temp.TipoPeriocidadPagoId = TipoPeriocidadPagoId;
                    temp.Asistencias = false;
                    temp.Tipo = 1;
                    temp.CompanyId = CompanyId;

                    var periodo = await _repositoryAsync.AddAsync(temp);
                }
            }
            return new Response<int>(1);

        }

        private async Task<Response<int>> CreateSemanal(int Anio, int CompanyId, int TipoPeriocidadPagoId)
        {
            int semana = 1;

            for (int i = 1; i <= 12; i++)
            {
                int daysInMonth = DateTime.DaysInMonth(Anio, i);
                DateTime inicioMes = new DateTime(Anio, i, 1);
                DateTime finMes = new DateTime(Anio, i, daysInMonth);

                DateTime fecha = inicioMes;
                while (fecha <= finMes)
                {
                    if (fecha.DayOfWeek == DayOfWeek.Sunday)
                    {
                        DateTime inicioSemana = fecha;
                        DateTime finSemana = fecha.AddDays(6);
                        var temp = new Periodo();
                        temp.Desde = inicioSemana;
                        temp.Hasta = finSemana;
                        temp.Etapa = (Anio * 100) + semana++;
                        temp.TipoPeriocidadPagoId = TipoPeriocidadPagoId;
                        temp.Asistencias = false;
                        temp.Tipo = 1;
                        temp.CompanyId = CompanyId;

                        var periodo = await _repositoryAsync.AddAsync(temp);
                        //Console.WriteLine(inicioSemana.ToString() + "------" + finSemana.ToString());
                    }

                    fecha = fecha.AddDays(1);
                }
            }

            return new Response<int>(1);
        }

        private async Task<Response<int>> CreateCatorcena(int Anio, int CompanyId, int TipoPeriocidadPagoId)
        {
            int semana = 1;
            DateTime inicioAnio = new DateTime(Anio, 1, 1);

            while (inicioAnio.Year == Anio)
            {
                if (inicioAnio.DayOfWeek == DayOfWeek.Sunday)
                {
                    DateTime inicioSemana = inicioAnio;
                    DateTime finSemana = inicioAnio.AddDays(13); // 2 semanas = 14 días - 1 día (domingo) = 13 días
                    var temp = new Periodo();
                    temp.Desde = inicioSemana;
                    temp.Hasta = finSemana;
                    temp.Etapa = (Anio * 100) + semana++;
                    temp.TipoPeriocidadPagoId = TipoPeriocidadPagoId;
                    temp.Asistencias = false;
                    temp.Tipo = 1;
                    temp.CompanyId = CompanyId;

                    var periodo = await _repositoryAsync.AddAsync(temp);
                    //Console.WriteLine(inicioSemana.ToString() + "------" + finSemana.ToString());

                    inicioAnio = finSemana.AddDays(1); // Saltar al siguiente día después del período de 2 semanas
                }
                else
                {
                    inicioAnio = inicioAnio.AddDays(1);
                }
            }

            return new Response<int>(1);
        }



        private async Task<Response<int>> CreateQuincenas(int Anio, int CompanyId, int TipoPeriocidadPagoId)
        {
            var flag = false;

            for (int i = 1; i <= 24; i++)
            {

                var temp = new Periodo();

                if (i % 2 == 0)
                {

                    temp.Desde = new DateTime(Anio, i / 2, 16);
                    temp.Hasta = new DateTime(Anio, i / 2, DateTime.DaysInMonth(Anio, i / 2));

                }
                else
                {

                    temp.Desde = new DateTime(Anio, (i + 1) / 2, 01);
                    temp.Hasta = new DateTime(Anio, (i + 1) / 2, 15);

                }

                temp.TipoPeriocidadPagoId = TipoPeriocidadPagoId;
                temp.Etapa = (Anio * 100) + i;
                temp.Asistencias = false;

                if (!flag)
                {
                    temp.Estatus = 1;
                    flag = true;
                }
                else
                    temp.Estatus = 0;

                temp.Tipo = 1;
                temp.CompanyId = CompanyId;

                var periodo = await _repositoryAsync.AddAsync(temp);

            }

            return new Response<int>(1);

        }

        private async Task<Response<int>> CreateMensual(int Anio, int CompanyId, int TipoPeriocidadPagoId)
        {
            for (int i = 1; i <= 12; i++)
            {
                int daysInMonth = DateTime.DaysInMonth(Anio, i);
                DateTime inicioMes = new DateTime(Anio, i, 1);
                DateTime finMes = new DateTime(Anio, i, daysInMonth);

                var temp = new Periodo();
                temp.Desde = inicioMes;
                temp.Hasta = finMes;
                temp.Etapa = Anio * 100 + i; // Usar el mes como número de etapa
                temp.TipoPeriocidadPagoId = TipoPeriocidadPagoId;
                temp.Asistencias = false;
                temp.Tipo = 1;
                temp.CompanyId = CompanyId;

                var periodo = await _repositoryAsync.AddAsync(temp);
                //Console.WriteLine(inicioMes.ToString() + "------" + finMes.ToString());
            }

            return new Response<int>(1);
        }


        private async Task<Response<int>> CreateBimestral(int Anio, int CompanyId, int TipoPeriocidadPagoId)
        {
            for (int i = 1; i <= 12; i += 2) // Incrementar de 2 en 2 para generar periodos bimestrales
            {
                int daysInFirstMonth = DateTime.DaysInMonth(Anio, i);
                int daysInSecondMonth = DateTime.DaysInMonth(Anio, i + 1);

                DateTime inicioPrimerMes = new DateTime(Anio, i, 1);
                DateTime finPrimerMes = new DateTime(Anio, i, daysInFirstMonth);

                DateTime inicioSegundoMes = new DateTime(Anio, i + 1, 1);
                DateTime finSegundoMes = new DateTime(Anio, i + 1, daysInSecondMonth);

                var temp = new Periodo();
                temp.Desde = inicioPrimerMes;
                temp.Hasta = finSegundoMes;
                temp.Etapa = Anio * 100 + i; // Usar el número de mes como número de etapa
                temp.TipoPeriocidadPagoId = TipoPeriocidadPagoId;
                temp.Asistencias = false;
                temp.Tipo = 1;
                temp.CompanyId = CompanyId;

                var periodo = await _repositoryAsync.AddAsync(temp);
                //Console.WriteLine(inicioPrimerMes.ToString() + "------" + finSegundoMes.ToString());
            }

            return new Response<int>(1);
        }

        private async Task<Response<int>> CreateDecenal(int Anio, int CompanyId, int TipoPeriocidadPagoId)
        {
            int periodo = 1;
            DateTime inicioAnio = new DateTime(Anio, 1, 1);

            while (inicioAnio.Year == Anio)
            {
                DateTime inicioPeriodo = inicioAnio;
                DateTime finPeriodo = inicioAnio.AddDays(9); // 10 días para un período decenal
                var temp = new Periodo();
                temp.Desde = inicioPeriodo;
                temp.Hasta = finPeriodo;
                temp.Etapa = (Anio * 100) + periodo++;
                temp.TipoPeriocidadPagoId = TipoPeriocidadPagoId;
                temp.Asistencias = false;
                temp.Tipo = 1;
                temp.CompanyId = CompanyId;

                //var periodo = await _repositoryAsync.AddAsync(temp);
                Console.WriteLine(inicioPeriodo.ToString() + "------" + finPeriodo.ToString());

                inicioAnio = finPeriodo.AddDays(1); // Saltar al siguiente día después del período decenal
            }

            return new Response<int>(1);
        }


        public int DateTimeToQuincena(DateTime fecha)
        {

            // Obtener el año y la quincena
            int año = fecha.Year;
            int quincena = (fecha.Month - 1) / 2 + 1;

            // Crear la cadena de texto de la quincena
            string quincenaFormato = $"{año}{quincena:D2}";

            return int.Parse(quincenaFormato);
        }

        public DateTime QuincenaToDateTime(int quincena_int)
        {
            string quincenaString = quincena_int.ToString();

            // Obtener el año y el número de quincena
            int año = int.Parse(quincenaString.Substring(0, 4));
            int quincena = int.Parse(quincenaString.Substring(4, 2));

            // Calcular el primer día de la quincena
            DateTime primerDiaQuincena = new DateTime(año, quincena * 2 - 1, 1);

            return primerDiaQuincena;
        }

        public int GetQuincenaFinalByPlazoQuincenal(int quincena_inicial,int plazo_quincenal)
        {
            int añoInicial = quincena_inicial / 100;
            int quincenaInicialNum = quincena_inicial % 100;

            int añoFinal = añoInicial;
            int quincenaFinalNum = quincenaInicialNum + plazo_quincenal;

            while (quincenaFinalNum > 24)
            {
                añoFinal++;
                quincenaFinalNum -= 24;
            }

            return añoFinal * 100 + quincenaFinalNum;
        }


    }
}
