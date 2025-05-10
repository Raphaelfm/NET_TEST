using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleService _saleService;
        private readonly ILogger<CreateSaleHandler> _logger;

        public CreateSaleHandler(ISaleService saleService, ILogger<CreateSaleHandler> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale
            {
                SaleNumber = Guid.NewGuid().ToString()[..8],
                SaleDate = DateTime.UtcNow,
                CustomerName = request.Request.CustomerName,
                BranchName = request.Request.BranchName
            };

            foreach (var item in request.Request.Items)
            {
                if (item.Quantity > 20)
                    throw new InvalidOperationException("Cannot sell more than 20 items of a product.");

                decimal discount = 0;
                if (item.Quantity >= 10) discount = 0.2m * item.Quantity * item.UnitPrice;
                else if (item.Quantity >= 4) discount = 0.1m * item.Quantity * item.UnitPrice;

                sale.Items.Add(new SaleItem
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = discount
                });
            }

            var created = await _saleService.CreateAsync(sale);
            _logger.LogInformation("[SaleCreated] {SaleNumber}", created.SaleNumber);

            return created.Id;
        }
    }    
}