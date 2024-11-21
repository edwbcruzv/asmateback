using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.Reembolsos.Commands.CreateReembolsoCommand
{
    public class CreateReembolsoCommandValidator : AbstractValidator<CreateReembolsoCommand>
    {
        public readonly IRepositoryAsync<Company> _repositoryAsyncCompany;

        public CreateReembolsoCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany)
        {
            _repositoryAsyncCompany = repositoryAsyncCompany;

            RuleFor(r => r.CompanyId)
                .NotEmpty()
                .WithMessage("CompanyId es obligatorio")
                .MustAsync(async (CompanyId, cancellationToken) =>
                {
                    var company = await _repositoryAsyncCompany.GetByIdAsync(CompanyId);

                    if (company == null) return false;

                    return true;
                })
                .WithMessage($"Registro CompanyId no encontrado en companies");

            RuleFor(r => r.Descripcion)
                .NotEmpty()
                .WithMessage("La Descricpcion es obligatorio.")
                .MaximumLength(200)
                .WithMessage("Se supero los 200 caracteres.");

            //RuleFor(r => r.Clabe) 
            //    .NotEmpty()
            //    .WithMessage("La Clabe es obligatorio")
            //    .Length(18)
            //    .WithMessage("Las Clabe debe de tener 18 numero")
            //    .Matches("^[0-9]+$").WithMessage("La Clabe solo debe contener números");


        }
    }
}
