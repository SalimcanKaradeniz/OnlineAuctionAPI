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
        public int LangId { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string ShortDescription { get; set; }
        [MaxLength(250)]
        public string Detail { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public int? Rank { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
