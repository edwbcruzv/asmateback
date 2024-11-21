using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Comprobantes.Commands.UpdateComprobanteCommand
{
    public class UpdateComprobanteCommandValidator : AbstractValidator<UpdateComprobanteCommand>
    {
        private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;

        public UpdateComprobanteCommandValidator(IRepositoryAsync<Viatico> repositoryAsyncViatico)
        {
            _repositoryAsyncViatico = repositoryAsyncViatico;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El Id es obligatorio");

            RuleFor(x => x.ViaticoId)
                //.Empty().When(x => x.ViaticoId != null).WithMessage("El viatico es opcional")
                .MustAsync(async (ViaticoId, cancellationToken) =>
                {
                    Console.WriteLine(ViaticoId);
                    if (ViaticoId == 0) return true; 
                    if (ViaticoId == null) return true;  // Permitir valores nulos sin realizar la validación

                    var item = await _repositoryAsyncViatico.GetByIdAsync(ViaticoId);

                    return item != null;
                })
                .WithMessage("El viatico no existe");


        }
    }
}
