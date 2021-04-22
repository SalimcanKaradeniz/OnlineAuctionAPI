using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class ProductsModel
    {
        public ProductsModel()
        {
            this.CreatedAt = DateTime.Now;
        }

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
        public int? Rank { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
