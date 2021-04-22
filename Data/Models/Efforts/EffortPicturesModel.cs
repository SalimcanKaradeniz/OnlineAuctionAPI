using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Data.Models
{
    public class EffortPicturesModel
    {
        public int Id { get; set; }
        public int LangId { get; set; }
        public int EffortId { get; set; }
        [MaxLength]
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }

        public EffortsModel Effort { get; set; }
    }

}
