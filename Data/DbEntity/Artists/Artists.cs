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
            this.CreatedAt = DateTime.Now;
            this.Efforts = new List<Efforts>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string NameSurname { get; set; }
        [MaxLength]
        public string About_tr { get; set; }
        [MaxLength]
        public string About_en { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Efforts> Efforts { get; set; }
    }
}
