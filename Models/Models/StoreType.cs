using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Store_Type")]
    public partial class StoreType
    {
        public StoreType()
        {
            Stores = new HashSet<Store>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("Type")]
        public virtual ICollection<Store> Stores { get; set; }
    }
}
