﻿using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MiPortal.Prestamos
{
    public class PrestamoDTO
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int PeriodoInicial { get; set; }
        public int? PeriodoFinal { get; set; }
        public EstatusOperacion Estatus { get; set; }

        public DateTime FechaEstatusPendiente { get; set; }
        public DateTime FechaEstatusActivo { get; set; }
        public DateTime FechaEstatusInactivo { get; set; }
        public DateTime FechaEstatusFiniquitado { get; set; }

        public int Plazo { get; set; }
        public float Monto { get; set; }
        public float MontoPagado { get; set; }
        public float Interes { get; set; }
        public float TazaInteres { get; set; }
        public float PlazoInteres { get; set; }
        public float FondoGarantia { get; set; }
        public float TazaFondoGarantia { get; set; }
        public DateTime FechaTransferencia { get; set; }
        public float Descuento { get; set; }
        public float Total { get; set; }
        public TipoPrestamo Tipo { get; set; }

        public string? SrcDocAcuseFirmado { get; set; }
        public string? SrcDocPagare { get; set; }
        public string? SrcDocConstanciaRetiro { get; set; }
        public string? SrcDocContanciaTransferencia { get; set; }

    }
}
