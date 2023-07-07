using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Buyer_Id")]
        public int BuyerId { get; set; }
        [Column("Admin_Engineer_Id")]
        public int AdminEngineerId { get; set; }
        [Column("Executive_Team_Id")]
        public int? ExecutiveTeamId { get; set; }
        [Column("Order_Date", TypeName = "date")]
        public DateTime OrderDate { get; set; }

        [ForeignKey("AdminEngineerId")]
        [InverseProperty("OrderAdminEngineers")]
        public virtual Engineer AdminEngineer { get; set; } = null!;
        [ForeignKey("BuyerId")]
        [InverseProperty("Orders")]
        public virtual Buyer Buyer { get; set; } = null!;
        [ForeignKey("ExecutiveTeamId")]
        [InverseProperty("OrderExecutiveTeams")]
        public virtual Engineer? ExecutiveTeam { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
