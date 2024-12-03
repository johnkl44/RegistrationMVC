using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationMVC.Models
{
    public class RegisterModel
    {
        [Key]
        public int UserID { get; set; }

        [DisplayName("Product Name")]
        public string FirstName {  get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [DisplayName("Password")]
        public string PasswordHash { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}