using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class SliderRequestModel
    {
        public SlidersModel Slider { get; set; }
    }
}
