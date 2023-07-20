namespace AgroCare.Data.DTOs
{
    public class PurchaseDetailDto : BasePropertiesDto
    {
        public string Item { get; set; } = null!;

        public int Quantity { get; set; }

        public float Price { get; set; }
        public string? Details { get; set; }
    }
}
