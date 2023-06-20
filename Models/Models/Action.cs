using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Action")]
    public partial class Action
    {
        public Action()
        {
            Steps = new HashSet<Step>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("Action")]
        public virtual ICollection<Step> Steps { get; set; }
    }
}
