using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Step")]
    public partial class Step
    {
        public Step()
        {
            StepDetails = new HashSet<StepDetail>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Plan_Id")]
        public int PlanId { get; set; }
        [Column("Action_Id")]
        public int ActionId { get; set; }
        [Column("Estimated_Start_Date", TypeName = "date")]
        public DateTime EstimatedStartDate { get; set; }
        [Column("Estimated_Finish_Date", TypeName = "date")]
        public DateTime EstimatedFinishDate { get; set; }
        [Column("Is_Checked")]
        public bool IsChecked { get; set; }

        [ForeignKey("ActionId")]
        [InverseProperty("Steps")]
        public virtual Action Action { get; set; } = null!;
        [ForeignKey("PlanId")]
        [InverseProperty("Steps")]
        public virtual Plan Plan { get; set; } = null!;
        [InverseProperty("Step")]
        public virtual ICollection<StepDetail> StepDetails { get; set; }
    }
}
