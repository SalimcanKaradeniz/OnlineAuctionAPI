using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Model
{
    public class NewsRequestModel
    {
        public NewsModel News { get; set; }
    }
}
