using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository repository, ILogger<SaleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Sale> CreateAsync(Sale sale)
        {
            _logger.LogInformation("Creating sale {SaleNumber}", sale.SaleNumber);
            await _repository.AddAsync(sale);
            return sale;
        }

        public async Task CancelAsync(Guid id)
        {
            var sale = await _repository.GetByIdAsync(id) ?? throw new Exception("Sale not found");
            sale.Cancel();
            _logger.LogInformation("Cancelling sale {SaleNumber}", sale.SaleNumber);
            await _repository.UpdateAsync(sale);
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Sale> UpdateAsync(Sale sale)
        {
            _logger.LogInformation("Updating sale {SaleNumber}", sale.SaleNumber);
            await _repository.UpdateAsync(sale);
            return sale;
        }
    }
}