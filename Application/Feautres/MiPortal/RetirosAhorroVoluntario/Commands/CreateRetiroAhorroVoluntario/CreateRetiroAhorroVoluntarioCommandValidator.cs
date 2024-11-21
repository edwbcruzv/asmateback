using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.CreateRetiroAhorroVoluntario
{
    public class CreateRetiroAhorroVoluntarioCommandValidator : AbstractValidator<CreateRetiroAhorroVoluntarioCommand>
    {
        private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;

        public CreateRetiroAhorroVoluntarioCommandValidator(IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario)
        {
            _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;

            RuleFor(x => x.AhorroVoluntarioId)
                .NotEmpty().WithMessage("El AhorroVoluntario es obligatorio")
                .MustAsync(async (AhorroVoluntarioId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncAhorroVoluntario.GetByIdAsync(AhorroVoluntarioId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El Ahorro Voluntario no existe");

            RuleFor(x => x.Cantidad)
                .NotEmpty().WithMessage("La cantidad es obligatorio");

            RuleFor(x => x.SeguirAhorrando)
                .NotNull().WithMessage("La SeguirAhorrando es obligatorio");

            RuleFor(x => x.Porcentaje)
                .InclusiveBetween(1,100).WithMessage("El porcentaje tiene que ser entre 1 o 100.")
                .NotEmpty().WithMessage("El porcentaje es obligatorio");

            RuleFor(x => x.FileSolicitudFirmado)
            .NotNull().WithMessage("El archivo es requerido")
            .NotEmpty().WithMessage("El archivo no debe estar vacío")
            .Must(BeValidFileType).WithMessage("El archivo debe ser de tipo PDF");
        }

        private bool BeValidFileType(IFormFile file)
        {
            if (file == null)
                return true; // La validación de tipo de archivo no es necesaria si el archivo es nulo

            var allowedExtensions = new[] { ".pdf" }; // Extensiones permitidas

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return allowedExtensions.Contains(fileExtension);
        }
    }
}
