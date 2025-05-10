using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateSaleRequest Request { get; set; }
        public UpdateSaleCommand(Guid id, UpdateSaleRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}