using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class ArtistsModel
    {
        public ArtistsModel()
        {
            this.CreatedAt = DateTime.Now;
            this.Efforts = new List<EffortsModel>();
        }

        public int Id { get; set; }
        public int LangId { get; set; }
        [MaxLength(250)]
        public string NameSurname { get; set; }
        [MaxLength]
        public string About { get; set; }
        [MaxLength]
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public int? Rank { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<EffortsModel> Efforts { get; set; }
    }
}

