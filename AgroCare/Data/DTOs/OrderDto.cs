namespace AgroCare.Data.DTOs
{
    public class OrderDto : BasePropertiesDto
    {
        public DateOnly OrderDate { get; set; }
        public EngineerDto AdminEngineer { get; set; } = null!;
        public BuyerDto Buyer { get; set; } = null!;
        public EngineerDto? ExecutiveTeam { get; set; }
        public bool ClickedByBuyer { get; set; }
        public bool ClickedByAdmin { get; set; }
        public bool ClickedByTeam { get; set; }
        public ICollection<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
    }
}
