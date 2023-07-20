namespace AgroCare.Data.DTOs
{
    public class PlanDto : BasePropertiesDto
    {
        public int OrderDetailsId { get; set; }
        public int LandId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly FinishDate { get; set; }
        public float Quantity { get; set; }
        public ICollection<StepDto> Steps { get; set; } = new List<StepDto>();
    }
}
