using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("ProductTypes")]
    public class ProductTypes
    {
        public ProductTypes()
        {
            this.Products = new List<Products>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }

        public List<Products> Products { get; set; }
    }
}

