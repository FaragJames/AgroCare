namespace AgroCare.ViewModels
{
    public class PlanViewModel
    {
        public int LandId { get; set; }
        public int OrderDetailId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly FinishDate { get; set; }
        public float Quantity { get; set; }
        public List<PlanStepDetailsView> planStepDetailsView { get; set; } = null!;
    }
}
