using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Comprobantes.Commands.CreateComprobanteSinXML
{
    public class CreateComprobanteSinXMLCommandValidator : AbstractValidator<CreateComprobanteSinXMLCommand>
    {
        private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;

        public CreateComprobanteSinXMLCommandValidator(IRepositoryAsync<Viatico> repositoryAsyncViatico)
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


        }
    }
}
