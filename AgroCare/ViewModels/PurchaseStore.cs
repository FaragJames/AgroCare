namespace AgroCare.ViewModels
{
    public class PurchaseStore
    {
        public bool IsOneFarmer { get; set; }
        public string? SearchId { get; set; }
        public List<PurchaseStoreDtai> PurchaseStoreDtais { get; set; } = new List<PurchaseStoreDtai>();
    }
    public class PurchaseStoreDtai
    {
        public string? Farmer { get; set; }
        public int? FarmerId { get; set; }
        public float Cost { get; set; }
        public DateOnly Date { get; set; }
    }
}
