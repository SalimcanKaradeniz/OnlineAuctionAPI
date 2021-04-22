using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Data.DbEntity
{
    public class PageSpecifications
    {
        public PageSpecifications()
        {
            this.Pages = new List<Pages>();
        }

        public int Id { get; set; }
        public int LangId { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Pages> Pages { get; set; }
    }
}