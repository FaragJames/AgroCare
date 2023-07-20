namespace AgroCare.Data.DTOs
{
    public class EngineerDto : BasePropertiesDto
    {
        public string UserName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public float Salary { get; set; }
        public int? HeadEngineerId { get; set; }
        public EngineerTypeDto EngineerType { get; set; } = null!;
        public ICollection<EngineerDto> InverseHeadEngineer { get; set; } = new List<EngineerDto>();
    }
}
