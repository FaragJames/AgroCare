namespace AgroCare.Data.DTOs
{
    public class OrderDetailDto : BasePropertiesDto
    {
        public int Kilos { get; set; }
        public float KiloPrice { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public ItemDto Item { get; set; } = null!;
    }
}
