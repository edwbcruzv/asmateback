﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Kanban.Sistemas
{
    public class SistemaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string? Color { get; set; }
        public string Estado { get; set; }
        public int EstadoId { get; set; }
    }
}
