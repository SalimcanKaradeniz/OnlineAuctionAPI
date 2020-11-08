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
        [MaxLength(250)]
        public string TR_Title { get; set; }
        [MaxLength(250)]
        public string EN_Title { get; set; }
        [MaxLength(250)]
        public string TR_Keywords { get; set; }
        [MaxLength(250)]
        public string EN_Keywords { get; set; }
        [MaxLength(250)]
        public string TR_Description { get; set; }
        [MaxLength(250)]
        public string EN_Description { get; set; }
        [MaxLength(500)]
        public string RedirectionLink { get; set; }
        [MaxLength]
        public string TR_Detail { get; set; }
        [MaxLength]
        public string EN_Detail { get; set; }
        public int SpecificationId { get; set; }
        public bool IsMain { get; set; }
        public bool IsFooter { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("SpecificationId")]
        public PageSpecifications PageSpecification { get; set; }
        public List<PageBanner> PageBanners { get; set; }
    }
}
