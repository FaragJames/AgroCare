using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Land_Type")]
    public partial class LandType : IBaseProperties
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("Type")]
        public virtual ICollection<Land> Lands { get; set; } = new List<Land>();
    }
}