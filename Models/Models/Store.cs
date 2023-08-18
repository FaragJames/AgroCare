using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Store")]
    public partial class Store : IBaseProperties, IUserName
    {
        [Key]
        public int Id { get; set; }

        [Column("Type_Id")]
        public int TypeId { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string UserName { get; set; } = null!;

        [StringLength(50)]
        public string Location { get; set; } = null!;

        [Column("Image_Path")]
        [StringLength(100)]
        public string ImagePath { get; set; } = null!;

        
        [InverseProperty("Store")]
        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

        [ForeignKey("TypeId")]
        [InverseProperty("Stores")]
        public virtual StoreType Type { get; set; } = null!;
    }
}