using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Data.Models
{
    public class NewsModel
    {
        public NewsModel()
        {
            this.CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        [MaxLength(250)]
        public string Title_tr { get; set; }
        [MaxLength(250)]
        public string Title_en { get; set; }
        [MaxLength(250)]
        public string ShortDescription_tr { get; set; }
        [MaxLength(250)]
        public string ShortDescription_en { get; set; }
        [MaxLength(250)]
        public string Detail_tr { get; set; }
        [MaxLength(250)]
        public string Detail_en { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
