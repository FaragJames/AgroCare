namespace AgroCare.Data.DTOs
{
    public class PlanDto : BasePropertiesDto
    {
        public OrderDetailDto OrderDetails { get; set; } = null!;
        public bool ClickByFarmer { get; set; }
        public LandDto Land { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly FinishDate { get; set; }
        public float Quantity { get; set; }
        public ICollection<StepDto> Steps { get; set; } = new List<StepDto>();
    }
}
