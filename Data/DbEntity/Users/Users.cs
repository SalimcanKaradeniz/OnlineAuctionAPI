using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineAuction.Data.DbEntity
{
    [Table("Users")]
    public class Users
    {
        public Users()
        {
            this.CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string UserName { get; set; }
        [MaxLength(250)]
        public string Password { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(250)]
        public string FirstName { get; set; }
        [MaxLength(250)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string LandPhone { get; set; }
        public int? IdentityNumber { get; set; }
        [MaxLength(50)]
        public string CellPhone { get; set; }
        [MaxLength(100)]
        public string CityName { get; set; }
        [MaxLength(100)]
        public string DistrictName { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int UserType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
