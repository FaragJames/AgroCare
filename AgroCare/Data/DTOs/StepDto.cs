namespace AgroCare.Data.DTOs
{
    public class StepDto : BasePropertiesDto
    {
        public DateOnly EstimatedStartDate { get; set; }
        public DateOnly EstimatedFinishDate { get; set; }
        public bool IsChecked { get; set; }
        public ActionDto Action { get; set; } = null!;
        public ICollection<StepDetailDto> StepDetails { get; set; } = new List<StepDetailDto>();
    }
}
