using Application.DTOs.Administracion;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFileToRarService
    {
        public Task<Response<SourceFileDto>> createRarFacturas(int[] ids); 
        public Task<Response<SourceFileDto>> createRarComplementoPago(int[] ids);
        public Task<string> createRarReembolso(Dictionary<int, Dictionary<int, List<string>>> diccionarioReembolsos);
    }
}
