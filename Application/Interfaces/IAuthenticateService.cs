using Application.DTOs;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(String NickName, string password);
    }
}
