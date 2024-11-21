using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.UpdateReembolsoCommand
{
    public class UpdateReembolsoCommandValidator : AbstractValidator<UpdateReembolsoCommand>
    {
        private readonly IRepositoryAsync<Reembolso> _repositoryAsync;

        public UpdateReembolsoCommandValidator(IRepositoryAsync<Reembolso> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;

            //RuleFor(r => r.Clabe) // 18 numeros
            //   .NotEmpty()
            //   .WithMessage("La Clabe es obligatorio")
            //   .Length(18)
            //   .WithMessage("Las Clabe debe de tener 18 numero")
            //   .Matches("^[0-9]+$").WithMessage("La Clabe solo debe contener números");

            RuleFor(r => r.Descripcion)
                .NotEmpty()
                .WithMessage("La Descricpcion es obligatorio.")
                .MaximumLength(200)
                .WithMessage("Se supero los 200 caracteres.");

            RuleFor(r => r.EstatusId)
                .NotEmpty()
                .WithMessage("El estatus es obligatorio.");
                //.MaximumLength(20)
                //.WithMessage("Se supero los 20 caracteres.");


        }
    }
}
