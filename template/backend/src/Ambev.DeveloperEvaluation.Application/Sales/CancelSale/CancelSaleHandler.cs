using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand>
    {
        private readonly ISaleService _saleService;
        private readonly ILogger<CancelSaleHandler> _logger;

        public CancelSaleHandler(ISaleService saleService, ILogger<CancelSaleHandler> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        public async Task<Unit> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            await _saleService.CancelAsync(command.Id);
            _logger.LogInformation("[SaleCancelled] {SaleId}", command.Id);
            return Unit.Value;
        }

        Task IRequestHandler<CancelSaleCommand>.Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}