using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Step_Details")]
    public partial class StepDetail
    {
        [Key]
        public int Id { get; set; }

        [Column("Step_Id")]
        public int StepId { get; set; }

        [Column("Agricultural_Item_Id")]
        public int AgriculturalItemId { get; set; }

        public float Quantity { get; set; }

        [ForeignKey("AgriculturalItemId")]
        [InverseProperty("StepDetails")]
        public virtual AgriculturalItem AgriculturalItem { get; set; } = null!;

        [ForeignKey("StepId")]
        [InverseProperty("StepDetails")]
        public virtual Step Step { get; set; } = null!;
    }
}