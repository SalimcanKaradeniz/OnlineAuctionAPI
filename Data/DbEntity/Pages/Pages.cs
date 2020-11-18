using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("Pages")]
    public class Pages
    {
        public Pages()
        {
            this.PageBanners = new List<PageBanner>();
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int? Rank { get; set; }
        [MaxLength(250)]
        public string Title_tr { get; set; }
        [MaxLength(250)]
        public string Title_en { get; set; }
        [MaxLength(250)]
        public string Keywords_tr { get; set; }
        [MaxLength(250)]
        public string Keywords_en { get; set; }
        [MaxLength(250)]
        public string Description_tr { get; set; }
        [MaxLength(250)]
        public string Description_en { get; set; }
        [MaxLength]
        public string Detail_tr { get; set; }
        [MaxLength]
        public string Detail_en { get; set; }
        [MaxLength(500)]
        public string RedirectionLink { get; set; }
        public int SpecificationId { get; set; }
        public bool IsActive { get; set; }
        public bool IsMain { get; set; }
        public bool IsFooter { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("SpecificationId")]
        public PageSpecifications PageSpecification { get; set; }
        public List<PageBanner> PageBanners { get; set; }
    }
}
