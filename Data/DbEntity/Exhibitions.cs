using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("Exhibitions")]
    public class Exhibitions
    {
        public Exhibitions()
        {
            this.Efforts = new List<Efforts>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string TR_Title { get; set; }
        [MaxLength(250)]
        public string EN_Title { get; set; }
        [MaxLength(250)]
        public string ExhibitionPlace { get; set; }
        [MaxLength(500)]
        public string TR_ShortDescription { get; set; }
        [MaxLength(500)]
        public string EN_ShortDescription { get; set; }
        [MaxLength(500)]
        public string LongDescription { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        [MaxLength]
        public string PdfLink { get; set; }
        public bool? IsOnline { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Efforts> Efforts { get; set; }
    }
}
