using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("Efforts")]
    public class Efforts
    {
        public Efforts()
        {
            this.EffortPictures = new List<EffortPictures>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string TR_EffortName { get; set; }
        [MaxLength(250)]
        public string EN_EffortName { get; set; }
        [MaxLength(250)]
        public string TR_UsedTechnic { get; set; }
        [MaxLength(250)]
        public string EN_UsedTechnic { get; set; }
        [MaxLength(250)]
        public string ArtistTitle { get; set; }
        public int? ArtistId { get; set; }
        public int? AuctionId { get; set; }
        public int? ExhibitionId { get; set; }
        public int? LotNumber { get; set; }
        public bool? IsSignature { get; set; }
        public decimal? DimensionWidth { get; set; }
        public decimal? DimensionHeight { get; set; }
        public int? Year { get; set; }
        public decimal? StartPriceTry { get; set; }
        public decimal? StartPriceEur { get; set; }
        public int? View { get; set; }
        [MaxLength(250)]
        public string PictureAbout { get; set; }
        public DateTime CreatedAt { get; set; }

        public Artists Artist { get; set; }
        public Exhibitions Exhibition { get; set; }
        public List<EffortPictures> EffortPictures { get; set; }
    }
}
