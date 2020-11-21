using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class ProductCategoryModel
    {
        public ProductCategoryModel()
        {
            this.CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength]
        public string ShortDescription_tr { get; set; }
        [MaxLength]
        public string ShortDescription_en { get; set; }
        [MaxLength]
        public string PictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ProductCategoryRequestModel
    {
        public ProductCategoryModel ProductCategory { get; set; }
    }
}

