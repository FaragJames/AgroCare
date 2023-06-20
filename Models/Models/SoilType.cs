using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Soil_Type")]
    public partial class SoilType
    {
        public SoilType()
        {
            Lands = new HashSet<Land>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("SoilType")]
        public virtual ICollection<Land> Lands { get; set; }
    }
}
