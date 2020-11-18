using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("LogSystem")]
    public class LogSystem
    {
        public LogSystem()
        {
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        [MaxLength]
        public string Url { get; set; }
        [MaxLength]
        public string ShortDescription { get; set; }
        [MaxLength]
        public string Detail { get; set; }
        [MaxLength]
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
