using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Step")]
    public partial class Step : IBaseProperties
    {
        [Key]
        public int Id { get; set; }

        [Column("Plan_Id")]
        public int PlanId { get; set; }

        [Column("Action_Id")]
        public int ActionId { get; set; }

        [Column("Estimated_Start_Date", TypeName = "date")]
        public DateOnly EstimatedStartDate { get; set; }

        [Column("Estimated_Finish_Date", TypeName = "date")]
        public DateOnly EstimatedFinishDate { get; set; }

        [Column("Is_Checked")]
        public bool IsChecked { get; set; }

        [ForeignKey("ActionId")]
        [InverseProperty("Steps")]
        public virtual Action Action { get; set; } = null!;

        
        [ForeignKey("PlanId")]
        [InverseProperty("Steps")]
        public virtual Plan Plan { get; set; } = null!;

        [InverseProperty("Step")]
        public virtual ICollection<StepDetail> StepDetails { get; set; } = new List<StepDetail>();
    }
}