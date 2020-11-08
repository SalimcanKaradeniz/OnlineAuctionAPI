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
        public string Address { get; set; }
        public int SocialMediaTypesId { get; set; }
        public DateTime CreatedAt { get; set; }

        public SocialMediaTypes SocialMediaTypes { get; set; }
    }
}
