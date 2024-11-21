using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteValeAzulCommand
{
    public class CreateComprobanteValeAzulCommandValidator : AbstractValidator<CreateComprobanteValeAzulCommand>
    {
        private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;

        public CreateComprobanteValeAzulCommandValidator(IRepositoryAsync<Viatico> repositoryAsyncViatico)
        {
            _repositoryAsyncViatico = repositoryAsyncViatico;

            RuleFor(x => x.ViaticoId)
                .NotEmpty().WithMessage("El campo ViaticoId es obligatorio")
                .MustAsync(async (ViaticoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncViatico.GetByIdAsync(ViaticoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El viatico no existe");

            RuleFor(x => x.FechaMovimiento)
                .NotEmpty().WithMessage("El campo FechaMovimiento es obligatorio");

            RuleFor(x => x.Concepto)
                .NotEmpty().WithMessage("El campo Concepto es obligatorio");

            RuleFor(x => x.MetodoPagoId)
                .NotEmpty().WithMessage("El campo MetodoPagoId es obligatorio");

        }
    }
}
