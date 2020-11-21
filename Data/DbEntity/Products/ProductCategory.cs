using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        public ProductCategory()
        {
            this.CreatedAt = DateTime.Now;
            this.Products = new List<Products>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength]
        public string ShortDescription_tr { get; set; }
        [MaxLength]
        public string ShortDescription_en { get; set; }
        [MaxLength]
        public string PictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Products> Products { get; set; }
    }
}

