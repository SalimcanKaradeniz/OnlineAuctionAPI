using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class PagesModel
    {
        public PagesModel()
        {
            this.PageBanners = new List<PageBannerModel>();
            this.CreatedAt = DateTime.Now;
        }

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

        public List<SubPageModel> SubPages { get; set; }

        [ForeignKey("SpecificationId")]
        public PageSpecificationsModel PageSpecification { get; set; }
        public List<PageBannerModel> PageBanners { get; set; }
    }

    public partial class SubPageModel
    {
        public SubPageModel()
        {
            this.PageBanners = new List<PageBannerModel>();
            this.CreatedAt = DateTime.Now;
        }

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
        public PageSpecificationsModel PageSpecification { get; set; }
        public List<PageBannerModel> PageBanners { get; set; }
    }
}