using Models.Models;

namespace AgroCare.ViewModels
{
    public class Plsn_Step_Oreder_DetailsView
    {
        public List<Models.Models.Action> Actions { get; set; } = null!;
        public List<AgriculturalItem> AgriculturalItems { get; set; } = null!;
        public List<OrderDetail> orderdetails { get; set; } = null!;
    }
}
