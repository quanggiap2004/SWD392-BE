using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    [Index(nameof(BoxName), IsUnique = true)]
    public class Box
    {
        [Key]
        public int BoxId { get; set; } // Primary Key

        [Required]
        public string BoxName { get; set; }

        public string? BoxDescription { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int SoldQuantity { get; set; }

        // Foreign Key to Brand
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        // Navigation Properties (if Box has relationships)
        public virtual ICollection<BoxOption> BoxOptions { get; set; }
        public virtual ICollection<BoxImage> BoxImages { get; set; }
        public virtual ICollection<BoxItem> BoxItems { get; set; }
        public virtual ICollection<OnlineSerieBox> OnlineSerieBoxes { get; set; }
    }
}