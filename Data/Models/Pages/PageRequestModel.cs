using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class PageRequestModel
    {
        public PagesModel Page { get; set; }
        public List<PagesModel> Pages { get; set; }
    }
}
