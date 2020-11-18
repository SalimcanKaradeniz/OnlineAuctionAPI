using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("Sliders")]
    public class Sliders
    {
        public Sliders()
        {
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title_tr { get; set; }
        [MaxLength(250)]
        public string Title_en { get; set; }
        [MaxLength]
        public string Summary_tr { get; set; }
        [MaxLength]
        public string Summary_en { get; set; }
        [MaxLength]
        public string Content_tr { get; set; }
        [MaxLength]
        public string Content_en { get; set; }
        [MaxLength]
        public string Link { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
