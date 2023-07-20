namespace AgroCare.Data.DTOs
{
    public class StepDetailDto : BasePropertiesDto
    {
        public float Quantity { get; set; }
        public AgriculturalItemDto AgriculturalItem { get; set; } = null!;
    }
}
