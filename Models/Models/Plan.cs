using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Plan")]
    public partial class Plan
    {
        public Plan()
        {
            Purchases = new HashSet<Purchase>();
            Steps = new HashSet<Step>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Order_Details_Id")]
        public int OrderDetailsId { get; set; }
        [Column("Land_Id")]
        public int LandId { get; set; }
        [Column("Start_Date", TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column("Finish_Date", TypeName = "date")]
        public DateTime FinishDate { get; set; }
        public float Quantity { get; set; }

        [ForeignKey("LandId")]
        [InverseProperty("Plans")]
        public virtual Land Land { get; set; } = null!;
        [ForeignKey("OrderDetailsId")]
        [InverseProperty("Plans")]
        public virtual OrderDetail OrderDetails { get; set; } = null!;
        [InverseProperty("Plan")]
        public virtual ICollection<Purchase> Purchases { get; set; }
        [InverseProperty("Plan")]
        public virtual ICollection<Step> Steps { get; set; }
    }
}
