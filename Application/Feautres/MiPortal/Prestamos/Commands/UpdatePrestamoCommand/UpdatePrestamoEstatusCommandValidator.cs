using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Prestamos.Commands.UpdatePrestamoCommand
{
    public class UpdatePrestamoEstatusCommandValidator : AbstractValidator<UpdatePrestamoEstatusCommand>
    {
       
        public UpdatePrestamoEstatusCommandValidator()
        {
            RuleFor(x => x.Estatus)
                .IsInEnum()
                .WithMessage("El estatus del prestamo no es valido");
        }
    }
}
