using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailBankManagement.Models
{
    public class CreateCustomerAccounts2por
    {


        [Required(ErrorMessage = "Please enter customer ID")]
        [Display(Name = "Customer ID")]
        [Range(100000000, 999999999, ErrorMessage = "customer ID must be a 9 digit number")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Please select account type")]
        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Please enter deposit amount")]
        [Display(Name = "Deposit Amount(INR)")]
        public int DepositAmount { get; set; }


    }
}