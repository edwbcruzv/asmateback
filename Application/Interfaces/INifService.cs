using Application.DTOs.NIF;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INifService
    {
        #region generadores

        public Task<Response<String>> Nif(DateTime fechaInicio, DateTime fechaFin, IFormFile file);
        #endregion

        #region beneficios
        public Task<List<NifResultadoDTO>> CalcularNif(DateTime fechaInicio, DateTime fechaFin, NifResultadoDTO employee, Nif nif);
        #endregion
    }
}
