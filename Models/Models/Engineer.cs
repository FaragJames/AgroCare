using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Engineer")]
    public partial class Engineer : IBaseProperties
    {
        [Key]
        public int Id { get; set; }

        [Column("Engineer_Type_Id")]
        public int EngineerTypeId { get; set; }

        [Column("Head_Engineer_Id")]
        public int? HeadEngineerId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; } = null!;

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string Phone { get; set; } = null!;

        public float Salary { get; set; }

        [ForeignKey("EngineerTypeId")]
        [InverseProperty("Engineers")]
        public virtual EngineerType EngineerType { get; set; } = null!;

        [ForeignKey("HeadEngineerId")]
        [InverseProperty("InverseHeadEngineer")]
        public virtual Engineer? HeadEngineer { get; set; }

        [InverseProperty("HeadEngineer")]
        public virtual ICollection<Engineer> InverseHeadEngineer { get; set; } = new List<Engineer>();

        [InverseProperty("AdminEngineer")]
        public virtual ICollection<Order> OrderAdminEngineers { get; set; } = new List<Order>();

        [InverseProperty("ExecutiveTeam")]
        public virtual ICollection<Order> OrderExecutiveTeams { get; set; } = new List<Order>();
    }
}