using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("FormSettings")]
    public class FormSettings
    {
        public FormSettings()
        {
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string MailCloud { get; set; }
        [MaxLength(250)]
        public string MailUserName { get; set; }
        [MaxLength(250)]
        public string MailPassword { get; set; }
        [MaxLength(250)]
        public string MailCloudPort { get; set; }
        public int? MailType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
