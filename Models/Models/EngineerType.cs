using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Engineer_Type")]
    public partial class EngineerType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("EngineerType")]
        public virtual ICollection<Engineer> Engineers { get; set; } = new List<Engineer>();
    }
}