using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class EffortsModel
    {
        public EffortsModel()
        {
            this.EffortPictures = new List<EffortPicturesModel>();
        }

        public int Id { get; set; }
        public int LangId { get; set; }
        [MaxLength(250)]
        public string EffortName { get; set; }
        [MaxLength(250)]
        public string UsedTechnic { get; set; }
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

        public ArtistsModel Artist { get; set; }
        public ExhibitionsModel Exhibition { get; set; }
        public List<EffortPicturesModel> EffortPictures { get; set; }
    }
}
