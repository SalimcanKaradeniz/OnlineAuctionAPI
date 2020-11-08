using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("PageBanner")]
    public class PageBanner
    {
        public PageBanner()
        {
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PageId { get; set; }
        [MaxLength]
        public string PictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("PageId")]
        public Pages Page { get; set; }
    }

}
