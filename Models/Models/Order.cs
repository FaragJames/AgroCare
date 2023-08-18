using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Order")]
    public partial class Order : IBaseProperties
    {
        [Key]
        public int Id { get; set; }

        [Column("Buyer_Id")]
        public int BuyerId { get; set; }

        [Column("Admin_Engineer_Id")]
        public int AdminEngineerId { get; set; }

        [Column("Executive_Team_Id")]
        public int? ExecutiveTeamId { get; set; }

        [Column("Order_Date", TypeName = "date")]
        public DateOnly OrderDate { get; set; }

        [Column("Clicked_By_Buyer")]
        public bool ClickedByBuyer { get; set; }

        [Column("Clicked_By_Admin")]
        public bool ClickedByAdmin { get; set; }

        [Column("Clicked_By_Team")]
        public bool ClickedByTeam { get; set; }

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
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}