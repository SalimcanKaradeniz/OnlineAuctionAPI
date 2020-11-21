using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("ProductCategoryGroup")]
    public class ProductCategoryGroup
    {
        public ProductCategoryGroup()
        {
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
