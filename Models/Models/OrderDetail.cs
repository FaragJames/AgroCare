using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Order_Details")]
    public partial class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Column("Order_Id")]
        public int OrderId { get; set; }

        [Column("Item_Id")]
        public int ItemId { get; set; }

        public int Kilos { get; set; }

        [Column("Kilo_Price")]
        public float KiloPrice { get; set; }

        [Column("Delivery_Date", TypeName = "date")]
        public DateTime DeliveryDate { get; set; }

        [ForeignKey("ItemId")]
        [InverseProperty("OrderDetails")]
        public virtual Item Item { get; set; } = null!;

        [ForeignKey("OrderId")]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; } = null!;

        [InverseProperty("OrderDetails")]
        public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}