using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("News")]
    public class News
    {
        public News()
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
        [MaxLength(250)]
        public string TR_ShortDescription { get; set; }
        [MaxLength(250)]
        public string TR_Detail { get; set; }
        [MaxLength(250)]
        public string EN_ShortDescription { get; set; }
        [MaxLength(250)]
        public string EN_Detail { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
