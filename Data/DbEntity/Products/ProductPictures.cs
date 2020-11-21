using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("ProductPictures")]
    public class ProductPictures
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [MaxLength]
        public string PictureUrl { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}

