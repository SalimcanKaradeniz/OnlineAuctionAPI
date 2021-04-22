using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class LanguagesModel
    {
        public LanguagesModel()
        {
            this.CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
