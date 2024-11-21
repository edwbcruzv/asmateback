using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Incidencias.Queries.GenerarIncidenciaPDFCommand
{
    public class GenerarIncidenciaPDFCommandValidator : AbstractValidator<GenerarIncidenciaPDFCommand>
    {
        public readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencias;

        public GenerarIncidenciaPDFCommandValidator(IRepositoryAsync<Incidencia> repositoryAsyncIncidencias)
        {
            _repositoryAsyncIncidencias = repositoryAsyncIncidencias;

            RuleFor(f => f.Id)
                .NotEmpty().WithMessage("El Id es obligatorio")
                .MustAsync(async (Id, cancellationToken) =>
                {
                    var incidencia = await _repositoryAsyncIncidencias.GetByIdAsync(Id);

                    if (incidencia == null) return false;

                    return true;
                })
                .WithMessage($"No se ha encontrado la Incidencia con el Id proporcionado");
        }
    }
}
