namespace AgroCare.Data.DTOs
{
    public class ItemDto : BasePropertiesDto
    {
        public string Name { get; set; } = null!;
        public double Price { get; set; }
    }
}