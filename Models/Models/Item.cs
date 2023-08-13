using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Item")]
    public partial class Item : IBaseProperties
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        public float Price { get; set; }

        [InverseProperty("Item")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}