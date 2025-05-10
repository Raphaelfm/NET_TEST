using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public List<SaleItem> Items { get; set; } = new();
        public SaleStatus Status { get; set; } = SaleStatus.Active;

        public decimal TotalAmount => CalculateTotal();

        private decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.TotalAmount;
            }
            return total;
        }

        public void Cancel() => Status = SaleStatus.Cancelled;
    }
}