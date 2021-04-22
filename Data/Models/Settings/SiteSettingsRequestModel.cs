using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class SiteSettingsModel
    {
        public SiteSettingsModel()
        {
            this.ComissionRate = 0M;
            this.TaxRate = 0;
            this.IsActive = false;
            this.CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public int LangId { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string CellPhone { get; set; }
        [MaxLength(50)]
        public string LandPhone { get; set; }
        [MaxLength(50)]
        public string Fax { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(250)]
        public string FacebookUrl { get; set; }
        [MaxLength(250)]
        public string InstagramUrl { get; set; }
        [MaxLength(250)]
        public string TwitterUrl { get; set; }
        [MaxLength(250)]
        public string LinkedinUrl { get; set; }
        [MaxLength(250)]
        public string PinterestUrl { get; set; }
        [MaxLength(250)]
        public string OtherSocialMediaUrl { get; set; }
        [MaxLength]
        public string Map { get; set; }
        [MaxLength]
        public string Logo { get; set; }
        [Required]
        public decimal ComissionRate { get; set; }
        [Required]
        public int TaxRate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [MaxLength(500)]
        public string GoogleRecaptchaSecretKey { get; set; }
        [MaxLength(500)]
        public string GoogleRecaptchaSiteKey { get; set; }
    }
}