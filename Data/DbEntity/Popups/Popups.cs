using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("Popups")]
    public class Popups
    {
        public Popups()
        {
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int LangId { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength]
        public string Description { get; set; }
        [MaxLength]
        public string RedirectionLink { get; set; }
        [MaxLength]
        public string PictureUrl { get; set; }
        public bool IsActive { get; set; }
        public int? Rank { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
