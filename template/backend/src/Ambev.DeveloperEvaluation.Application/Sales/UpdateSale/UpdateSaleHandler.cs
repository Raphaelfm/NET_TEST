using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, Guid>
    {
        private readonly ISaleService _saleService;
        private readonly ILogger<UpdateSaleHandler> _logger;

        public UpdateSaleHandler(ISaleService saleService, ILogger<UpdateSaleHandler> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        public async Task<Guid> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var existing = await _saleService.GetByIdAsync(command.Id) ?? throw new Exception("Sale not found");

            existing.CustomerName = command.Request.CustomerName;
            existing.BranchName = command.Request.BranchName;

            foreach (var item in command.Request.Items)
            {
                if (item.Quantity > 20)
                    throw new InvalidOperationException("Cannot sell more than 20 items of a product.");

                decimal discount = 0;
                if (item.Quantity >= 10) discount = 0.2m * item.Quantity * item.UnitPrice;
                else if (item.Quantity >= 4) discount = 0.1m * item.Quantity * item.UnitPrice;

                existing.Items.Where(x => x.Id == item.Id).FirstOrDefault()!.ProductName = item.ProductName;
                existing.Items.Where(x => x.Id == item.Id).FirstOrDefault()!.Quantity = item.Quantity;
                existing.Items.Where(x => x.Id == item.Id).FirstOrDefault()!.UnitPrice = item.UnitPrice;
                existing.Items.Where(x => x.Id == item.Id).FirstOrDefault()!.Discount = discount;
            }

            var updated = await _saleService.UpdateAsync(existing);
            _logger.LogInformation("[SaleModified] {SaleNumber}", updated.SaleNumber);
            return updated.Id;
        }
    }
}