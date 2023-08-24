namespace AgroCare.ViewModels
{
    public class PlanStepDetailsView
    {
        public float Quantity1 { get; set; }
        public int AgriculturalItemId { get; set; }
        public int ActionId { get; set; }
        public DateOnly EstimatedStartDate { get; set; }
        public DateOnly EstimatedFinishDate { get; set; }
    }
}
