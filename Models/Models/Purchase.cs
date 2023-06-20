using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Purchase")]
    public partial class Purchase
    {
        public Purchase()
        {
            PurchaseDetails = new HashSet<PurchaseDetail>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Store_Id")]
        public int StoreId { get; set; }
        [Column("Plan_Id")]
        public int PlanId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [ForeignKey("PlanId")]
        [InverseProperty("Purchases")]
        public virtual Plan Plan { get; set; } = null!;
        [ForeignKey("StoreId")]
        [InverseProperty("Purchases")]
        public virtual Store Store { get; set; } = null!;
        [InverseProperty("Purchase")]
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
