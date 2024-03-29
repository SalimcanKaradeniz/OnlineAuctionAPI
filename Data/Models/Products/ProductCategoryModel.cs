﻿using System;
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
            this.IsActive = false;
            this.CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public int LangId { get; set; }
        [MaxLength]
        public string Title_tr { get; set; }
        [MaxLength]
        public string Title_en { get; set; }
        public bool IsActive { get; set; }
        public int? Rank { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

