using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteCommand
{
    public class CreateComprobanteCommandValidator : AbstractValidator<CreateComprobanteCommand>
    {
        private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;

        public CreateComprobanteCommandValidator(IRepositoryAsync<Viatico> repositoryAsyncViatico)
        {
            _repositoryAsyncViatico = repositoryAsyncViatico;

            RuleFor(x => x.ViaticoId)
                .NotEmpty().WithMessage("El id viatico es oblicatorio es obligatorio")
                .MustAsync(async (ViaticoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncViatico.GetByIdAsync(ViaticoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El viatico no existe");

            //RuleFor(x => x.Total)
            //    .NotEmpty().WithMessage("La fecha es obligatorio");

            RuleFor(x => x.FechaMovimiento)
                .NotEmpty().WithMessage("La FechaMovimiento es obligatoria");

            RuleFor(x => x.Concepto)
                .NotEmpty().WithMessage("El Concepto es obligatorio");

            RuleFor(x => x.PDF)
                .NotNull()
                .WithMessage("El archivo PDF no puede ser nulo.");


        }
    }
}
