using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("SiteSettings")]
    public class SiteSettings
    {
        public SiteSettings() 
        {
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string TR_Title { get; set; }
        [MaxLength(250)]
        public string EN_Title { get; set; }
        [MaxLength(50)]
        public string CellPhone { get; set; }
        [MaxLength(50)]
        public string LandPhone { get; set; }
        [MaxLength(50)]
        public string Fax { get; set; }
        [MaxLength]
        public string Map { get; set; }
        [MaxLength]
        public string TR_Description { get; set; }
        [MaxLength]
        public string EN_Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
