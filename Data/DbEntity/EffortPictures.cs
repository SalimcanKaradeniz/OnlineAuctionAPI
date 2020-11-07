using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("EffortPictures")]
    public class EffortPictures
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EffortId { get; set; }
        [MaxLength]
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }

        public Efforts Effort { get; set; }
    }

}
