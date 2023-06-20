using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    [Table("Farmer")]
    public partial class Farmer
    {
        public Farmer()
        {
            Lands = new HashSet<Land>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; } = null!;
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        public string Phone { get; set; } = null!;

        [InverseProperty("Farmer")]
        public virtual ICollection<Land> Lands { get; set; }
    }
}
