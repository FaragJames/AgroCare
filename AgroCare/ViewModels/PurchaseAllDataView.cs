using System.ComponentModel.DataAnnotations;

namespace AgroCare.ViewModels
{
    public class PurchaseAllDataView
    {
        public int FarmerId { get; set; }
        public string Code { get; set; } = null!;
        public List<PurchaseDetailsView> PurchaseDetailsViews { get; set; } = null!;
    }
}