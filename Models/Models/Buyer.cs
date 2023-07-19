using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Buyer")]
    public partial class Buyer : IBaseProperties
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string UserName { get; set; } = null!;

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string Phone { get; set; } = null!;

        [InverseProperty("Buyer")]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}