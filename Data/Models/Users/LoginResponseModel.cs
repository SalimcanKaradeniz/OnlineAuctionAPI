using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Data.Models.Users
{
    public class LoginResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? IdentityNumber { get; set; }
        public string LandPhone { get; set; }
        public string CellPhone { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
    }
}
