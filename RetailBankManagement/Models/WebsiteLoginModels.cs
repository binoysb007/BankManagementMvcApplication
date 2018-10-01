using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RetailBankManagement.Models
{
    public class WebsiteLoginModels
    {
        [Required(ErrorMessage = "Please enter a valid user id")]
        [Display(Name = "User ID")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "User id must be more than 8 characters!")]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "User id must be alphabetic or alphanumeric")]
        public string User_ID { get; set; }

        [Required(ErrorMessage = "Please enter a valid password")]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Password must be more than 10 characters!")]
       [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "Password must be alphabetic or alphanumeric")]
        public string Password { get; set; }
    }
}