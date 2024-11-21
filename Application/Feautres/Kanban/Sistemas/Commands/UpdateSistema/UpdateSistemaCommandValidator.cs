using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Sistemas.Commands.UpdateSistema
{
    public class UpdateSistemaCommandValidator : AbstractValidator<UpdateSistemaCommand>
    {

        private readonly IRepositoryAsync<Estado> _repositoryAsyncEstado;

    public UpdateSistemaCommandValidator(IRepositoryAsync<Estado> repositoryAsyncEstado)
    {
        _repositoryAsyncEstado = repositoryAsyncEstado;

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del sistema es obligatorio");

        RuleFor(x => x.Clave)
            .NotEmpty().WithMessage("La clave del sistema es obligatorio");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripcion del sistema es obligatorio");

        RuleFor(x => x.EstadoId)
            .NotEmpty().WithMessage("TipoSolicitudTicketId es obligatorio")
            .MustAsync(async (TipoSolicitudTicketId, cancellationToken) =>
            {
                var item = await _repositoryAsyncEstado.GetByIdAsync(TipoSolicitudTicketId);

                if (item == null) return false;

                return true;
            })
            .WithMessage($"El estado no existe");


    }
}
}
