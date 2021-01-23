using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class TestModel
    {
        public string NameSurname { get; set; }
        [Required]
        public int year { get; set; }
        public IFormFile formFile { get; set; }
    }
}
