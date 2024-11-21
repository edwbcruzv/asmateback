using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Facturas
{
    public class EstatusCancelacionDto
    {
        public string CodigoEstatus { set; get; }
        public string Estado { set; get; }
        public string EstatusCancelacion { set; get; }
        public string EsCancelable { set; get; }
        public string Mensaje { set; get; }
    }
}
