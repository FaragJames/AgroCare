using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.Models.Auxiliary;

namespace Models.Models
{
    [Table("Purchase_Details")]
    public partial class PurchaseDetail : IBaseProperties
    {
        [Key]
        public int Id { get; set; }

        [Column("Purchase_Id")]
        public int PurchaseId { get; set; }

        [StringLength(50)]
        public string Item { get; set; } = null!;

        public int Quantity { get; set; }

        public float Price { get; set; }

        [StringLength(50)]
        public string? Details { get; set; }

        [ForeignKey("PurchaseId")]
        [InverseProperty("PurchaseDetails")]
        public virtual Purchase Purchase { get; set; } = null!;
    }
}