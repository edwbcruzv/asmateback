using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.SistemasDepartamentos.Commands.Create
{
    internal class CreateSistemaDepartamentoCommandValidator : AbstractValidator<CreateSistemaDepartamentoCommand>
    {
        private readonly IRepositoryAsync<Sistema> _repositoryAsyncSistema;
        private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;

        public CreateSistemaDepartamentoCommandValidator(IRepositoryAsync<Sistema> repositoryAsyncSistema, IRepositoryAsync<Departamento> repositoryAsyncDepartamento)
        {
            _repositoryAsyncSistema = repositoryAsyncSistema;
            _repositoryAsyncDepartamento = repositoryAsyncDepartamento;

            RuleFor(x => x.SistemaId)
               .NotEmpty().WithMessage("El sistema es oblicatorio")
               .MustAsync(async (SistemaId, cancellationToken) =>
               {
                   var item = await _repositoryAsyncSistema.GetByIdAsync(SistemaId);

                   if (item == null) return false;

                   return true;
               })
               .WithMessage($"El sistema no existe no existe");


            RuleFor(x => x.DepartamentoId)
               .NotEmpty().WithMessage("El departamento es oblicatorio")
               .MustAsync(async (DepartamentoId, cancellationToken) =>
               {
                   var item = await _repositoryAsyncDepartamento.GetByIdAsync(DepartamentoId);

                   if (item == null) return false;

                   return true;
               })
               .WithMessage($"El departamento no existe no existe");
        }
    }
}
