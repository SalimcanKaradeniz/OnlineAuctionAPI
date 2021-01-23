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
            this.IsActive = false;
            this.CreatedAt = DateTime.Now;
            this.Products = new List<Products>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength]
        public string Title_tr { get; set; }
        [MaxLength]
        public string Title_en { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Products> Products { get; set; }
    }
}

