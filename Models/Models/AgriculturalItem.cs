using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Agricultural_Item")]
    public partial class AgriculturalItem : IBaseProperties
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        
        [InverseProperty("AgriculturalItem")]
        public virtual ICollection<StepDetail> StepDetails { get; set; } = new List<StepDetail>();
    }
}