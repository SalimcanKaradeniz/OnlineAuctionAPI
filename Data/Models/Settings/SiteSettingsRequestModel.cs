using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Model
{
    public class SiteSettingsRequestModel
    {
        public SiteSettingsRequestModel() 
        {
            this.CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [MaxLength(50)]
        public string CellPhone { get; set; }
        [MaxLength(50)]
        public string LandPhone { get; set; }
        [MaxLength(50)]
        public string Fax { get; set; }
        [MaxLength]
        public string Map { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
