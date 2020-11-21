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
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string CellPhone { get; set; }
        [MaxLength(50)]
        public string LandPhone { get; set; }
        [MaxLength(50)]
        public string Fax { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(250)]
        public string FacebookUrl { get; set; }
        [MaxLength(250)]
        public string InstagramUrl { get; set; }
        [MaxLength(250)]
        public string TwitterUrl { get; set; }
        [MaxLength(250)]
        public string LinkedinUrl { get; set; }
        [MaxLength(250)]
        public string PinterestUrl { get; set; }
        [MaxLength(250)]
        public string OtherSocialMediaUrl { get; set; }
        [MaxLength]
        public string Map { get; set; }
        public string Logo { get; set; }
        public decimal? ComissionRate { get; set; }
        public int? TaxRate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
