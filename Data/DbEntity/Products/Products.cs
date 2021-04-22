using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("Products")]
    public class Products
    {
        public Products()
        {
            this.CreatedAt = DateTime.Now;
            this.ProductPictures = new List<ProductPictures>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int LangId { get; set; }
        public int CategoryId { get; set; }
        public int? ArtistId { get; set; }
        public int? Type { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public int? DiscountRate { get; set; }
        [MaxLength(100)]
        public string Code { get; set; }
        public int? Stock { get; set; }
        [MaxLength]
        public string Description { get; set; }
        [MaxLength]
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int? Rank { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("CategoryId")]
        public ProductCategory ProductCategory { get; set; }
        [ForeignKey("ArtistId")]
        public Artists Artist { get; set; }
        [ForeignKey("Type")]
        public ProductTypes ProductType { get; set; }
        public List<ProductPictures> ProductPictures { get; set; }
    }
}
