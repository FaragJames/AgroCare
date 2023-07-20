namespace AgroCare.Data.DTOs
{
    public class PurchaseDto : BasePropertiesDto
    {
        public DateOnly Date { get; set; }
        public PlanDto Plan { get; set; } = null!;
        public ICollection<PurchaseDetailDto> PurchaseDetails { get; set; } = new List<PurchaseDetailDto>();
        public StoreDto Store { get; set; } = null!;
    }
}
