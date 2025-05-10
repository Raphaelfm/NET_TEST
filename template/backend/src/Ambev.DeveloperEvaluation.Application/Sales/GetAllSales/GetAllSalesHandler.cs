using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, List<Sale>>
    {
        private readonly ISaleService _saleService;

        public GetAllSalesHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<List<Sale>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            return await _saleService.GetAllAsync();
        }
    }
}