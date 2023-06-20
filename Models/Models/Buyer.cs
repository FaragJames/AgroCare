using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Buyer")]
    public partial class Buyer
    {
        public Buyer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; } = null!;
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        public string Phone { get; set; } = null!;

        [InverseProperty("Buyer")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
