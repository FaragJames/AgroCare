using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Land")]
    public partial class Land : IBaseProperties
    {
        [Key]
        public int Id { get; set; }

        [Column("Farmer_Id")]
        public int FarmerId { get; set; }

        [Column("Type_Id")]
        public int TypeId { get; set; }

        [Column("Soil_Type_Id")]
        public int SoilTypeId { get; set; }

        [StringLength(50)]
        public string Location { get; set; } = null!;

        [Column("Has_Well")]
        public bool HasWell { get; set; }

        public float Area { get; set; }

        [ForeignKey("FarmerId")]
        [InverseProperty("Lands")]
        public virtual Farmer Farmer { get; set; } = null!;

        [InverseProperty("Land")]
        public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

        [ForeignKey("SoilTypeId")]
        [InverseProperty("Lands")]
        public virtual SoilType SoilType { get; set; } = null!;

        [ForeignKey("TypeId")]
        [InverseProperty("Lands")]
        public virtual LandType Type { get; set; } = null!;
    }
}