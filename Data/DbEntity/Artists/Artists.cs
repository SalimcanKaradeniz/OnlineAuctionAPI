using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("Artists")]
    public class Artists
    {
        public Artists()
        {
            this.Efforts = new List<Efforts>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string SurName { get; set; }
        [MaxLength]
        public string TR_About { get; set; }
        [MaxLength]
        public string EN_About { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        public bool IsOutHomePage { get; set; }
        public bool IsAbout { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Efforts> Efforts { get; set; }
    }
}
