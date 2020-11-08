using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class PageBannerModel
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        [MaxLength]
        public string PictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Id")]
        public PagesModel Page { get; set; }
    }

}
