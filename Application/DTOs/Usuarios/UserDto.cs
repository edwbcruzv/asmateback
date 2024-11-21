using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.DTOs.Usuarios
{
    public class UserDto
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string UserProfile { get; set; }
        public string UserEmail { get; set; }
        public byte UserType { get; set; }
        public string UserTipo { get; set; }
        public string Rol { get; set; }

    }
}
