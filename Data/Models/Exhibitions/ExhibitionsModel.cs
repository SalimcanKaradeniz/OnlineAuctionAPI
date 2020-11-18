using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Data.Models
{
    public class ExhibitionsModel
    {
        public ExhibitionsModel()
        {
            this.Efforts = new List<EffortsModel>();
        }

        public int Id { get; set; }
        [MaxLength(250)]
        public string TR_Title { get; set; }
        [MaxLength(250)]
        public string EN_Title { get; set; }
        [MaxLength(250)]
        public string ExhibitionPlace { get; set; }
        [MaxLength(500)]
        public string TR_ShortDescription { get; set; }
        [MaxLength(500)]
        public string EN_ShortDescription { get; set; }
        [MaxLength(500)]
        public string LongDescription { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        [MaxLength]
        public string PdfLink { get; set; }
        public bool? IsOnline { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<EffortsModel> Efforts { get; set; }
    }
}
