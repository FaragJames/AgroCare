namespace AgroCare.Data.DTOs
{
    public class LandDto : BasePropertiesDto
    {
        public string Location { get; set; } = null!;
        public bool HasWell { get; set; }
        public float Area { get; set; }
        public FarmerDto Farmer { get; set; } = null!;
        public SoilTypeDto SoilType { get; set; } = null!;
        public LandTypeDto Type { get; set; } = null!;
    }
}
