using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class PageSpecificationsModel
    {
        public PageSpecificationsModel()
        {
            this.Pages = new List<PagesModel>();
        }

        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<PagesModel> Pages { get; set; }
    }
}