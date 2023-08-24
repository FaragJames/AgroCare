namespace AgroCare.ViewModels
{
    public class PurchaseDetailsView
    {
        public string Item { get; set; } = null!;
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string? Details { get; set; }
        public bool IsRented { get; set; }
    }
}
