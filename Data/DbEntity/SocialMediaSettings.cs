using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("SocialMediaSettings")]
    public class SocialMediaSettings
    {
        public SocialMediaSettings()
        {
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string FacebookUrl { get; set; }
        [MaxLength(250)]
        public string TwitterUrl { get; set; }
        [MaxLength(250)]
        public string LinkedinUrl { get; set; }
        [MaxLength(250)]
        public string InstagramUrl { get; set; }
        [MaxLength(250)]
        public string PinterestUrl { get; set; }
        [MaxLength(250)]
        public string YoutubeUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
