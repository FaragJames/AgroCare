using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Agricultural_Item")]
    public partial class AgriculturalItem
    {
        public AgriculturalItem()
        {
            StepDetails = new HashSet<StepDetail>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("AgriculturalItem")]
        public virtual ICollection<StepDetail> StepDetails { get; set; }
    }
}
