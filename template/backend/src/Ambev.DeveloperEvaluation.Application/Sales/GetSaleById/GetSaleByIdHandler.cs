using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, Sale?>
    {
        private readonly ISaleService _saleService;

        public GetSaleByIdHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<Sale?> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _saleService.GetByIdAsync(request.Id);
        }
    }
}