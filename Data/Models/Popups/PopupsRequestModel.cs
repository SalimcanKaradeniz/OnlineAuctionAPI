using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class PopupsRequestModel 
    {
        public PopupsModel Popups { get; set; }
    }
    public class PopupsModel
    {
        public PopupsModel()
        {
            this.CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        [MaxLength(250)]
        public string Title_tr { get; set; }
        [MaxLength(250)]
        public string Title_en { get; set; }
        [MaxLength]
        public string Description_tr { get; set; }
        [MaxLength]
        public string Description_en { get; set; }
        [MaxLength]
        public string RedirectionLink { get; set; }
        [MaxLength]
        public string PictureUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
