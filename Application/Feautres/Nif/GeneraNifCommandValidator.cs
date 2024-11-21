using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Nif
{
    public class GeneraNifCommandValidator : AbstractValidator<GeneraNifCommand>
    {
        public GeneraNifCommandValidator()
        {
            RuleFor(p => p.FechaInicio).NotEmpty().WithMessage("FechaInicio es obligatorio");
            RuleFor(p => p.FechaFin).NotEmpty().WithMessage("FechaFin es obligatorio");
        }
    }
}
