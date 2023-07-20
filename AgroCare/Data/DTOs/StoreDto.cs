namespace AgroCare.Data.DTOs
{
    public class StoreDto : BasePropertiesDto
    {
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public StoreTypeDto Type { get; set; } = null!;
    }
}
