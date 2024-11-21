using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Puestos.Commands.CreatePuesto
{
    public class CreatePuestoCommandValidator : AbstractValidator<CreatePuestoCommand>
    {
        private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
        public CreatePuestoCommandValidator(IRepositoryAsync<Departamento> repositoryAsyncDepartamento)
        {
            _repositoryAsyncDepartamento = repositoryAsyncDepartamento;

            RuleFor(x => x.DepartamentoId)
                .NotEmpty().WithMessage("El DepartamentoId es obligatorio")
                .MustAsync(async (DepartamentoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncDepartamento.GetByIdAsync(DepartamentoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El DepartamentoId no existe");
        }
    }
}
