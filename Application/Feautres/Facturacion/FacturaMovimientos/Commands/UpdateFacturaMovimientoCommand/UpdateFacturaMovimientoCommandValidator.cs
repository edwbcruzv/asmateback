using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Facturacion.FacturaMovimientos.Commands.UpdateFacturaMovimientoCommand
{
    public class UpdateFacturaMovimientoCommandValidator : AbstractValidator<UpdateFacturaMovimientoCommand>
    {
        public readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
        public readonly IRepositoryAsync<UnidadMedida> _repositoryAsyncUnidadMedida;
        public readonly IRepositoryAsync<CveProducto> _repositoryAsyncCveProducto;
        public readonly IRepositoryAsync<ObjetoImpuesto> _repositoryAsyncObjetoImpuesto;
        public UpdateFacturaMovimientoCommandValidator(IRepositoryAsync<Factura> repositoryAsyncFactura,
            IRepositoryAsync<UnidadMedida> repositoryAsyncUnidadMedida, IRepositoryAsync<CveProducto> repositoryAsyncCveProducto,
            IRepositoryAsync<ObjetoImpuesto> repositoryAsyncObjetoImpuesto)
        {
            _repositoryAsyncFactura = repositoryAsyncFactura;
            _repositoryAsyncUnidadMedida = repositoryAsyncUnidadMedida;
            _repositoryAsyncCveProducto = repositoryAsyncCveProducto;
            _repositoryAsyncObjetoImpuesto = repositoryAsyncObjetoImpuesto;


            RuleFor(f => f.FacturaId)
                .NotEmpty().WithMessage("FacturaId es obligatorio")
                .MustAsync(async (FacturaId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncFactura.GetByIdAsync(FacturaId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro FacturaId no encontrado en facturas");

            RuleFor(f => f.Cantidad)
                .NotEmpty().WithMessage("Cantidad es obligatorio");

            RuleFor(f => f.UnidadMedidaId)
                .NotEmpty().WithMessage("UnidadMedidaId es obligatorio")
                .MustAsync(async (UnidadMedidaId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncUnidadMedida.GetByIdAsync(UnidadMedidaId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro UnidadMedidaId no encontrado en UnidadMedidas");

            RuleFor(f => f.CveProductoId)
                .NotEmpty().WithMessage("CveProductoId es obligatorio")
                .MustAsync(async (CveProductoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncCveProducto.GetByIdAsync(CveProductoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro CveProductoId no encontrado en CveProductos");

            RuleFor(f => f.ObjetoImpuestoId)
                .NotEmpty().WithMessage("ObjetoImpuestoId es obligatorio")
                .MustAsync(async (ObjetoImpuestoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncObjetoImpuesto.GetByIdAsync(ObjetoImpuestoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro ObjetoImpuestoId no encontrado en ObjetoImpuestos");

            RuleFor(f => f.Descripcion)
                .NotEmpty().WithMessage("Descripcion es obligatorio");


            RuleFor(f => f.PrecioUnitario)
                .NotEmpty().WithMessage("PrecioUnitario es obligatorio");

        }
    }
}
