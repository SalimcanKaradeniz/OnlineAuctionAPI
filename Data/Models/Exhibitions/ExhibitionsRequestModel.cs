using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Data.Models
{
    public class ExhibitionsRequestModel
    {
        public ExhibitionsRequestModel()
        {
            this.CreatedAt = DateTime.Now;
            this.Efforts = new List<EffortsModel>();
        }

        public int Id { get; set; }
        public int LangId { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string ExhibitionPlace { get; set; }
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        [MaxLength(500)]
        public string LongDescription { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        [MaxLength]
        public string PdfLink { get; set; }
        public bool? IsOnline { get; set; }
        public bool IsActive { get; set; }
        public int? Rank { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<EffortsModel> Efforts { get; set; }
    }
}
