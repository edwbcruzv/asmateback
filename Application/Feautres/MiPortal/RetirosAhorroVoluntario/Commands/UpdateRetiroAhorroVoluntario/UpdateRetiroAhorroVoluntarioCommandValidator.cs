using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.UpdateRetiroAhorroVoluntario
{
    public class UpdateRetiroAhorroVoluntarioCommandValidator : AbstractValidator<UpdateRetiroAhorroVoluntarioCommand>
    {
        private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;

        public UpdateRetiroAhorroVoluntarioCommandValidator(IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario)
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
        }
    }
}
