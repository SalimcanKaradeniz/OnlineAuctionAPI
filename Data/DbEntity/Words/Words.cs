using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("Words")]
    public class Words
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Sef { get; set; }
        [MaxLength(500)]
        public string Value_tr { get; set; }
        [MaxLength(500)]
        public string Value_en { get; set; }
    }
}
