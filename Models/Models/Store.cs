using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Store")]
    public partial class Store
    {
        [Key]
        public int Id { get; set; }

        [Column("Type_Id")]
        public int TypeId { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string Location { get; set; } = null!;

        [InverseProperty("Store")]
        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

        [ForeignKey("TypeId")]
        [InverseProperty("Stores")]
        public virtual StoreType Type { get; set; } = null!;
    }
}