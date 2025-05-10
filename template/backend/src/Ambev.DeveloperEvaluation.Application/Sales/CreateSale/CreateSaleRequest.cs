namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }    
}