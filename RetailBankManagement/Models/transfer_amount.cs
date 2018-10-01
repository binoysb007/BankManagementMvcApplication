using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RetailBankManagement.Models
{
    public class transfer_amount
    {
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        [Required(ErrorMessage = "Source account ID is required")] 
        [Display(Name = "Source account ID")] 
        public int source_account_id { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        [Required(ErrorMessage = "Target account ID is required")]
        [Display(Name = "Target account ID")] 
        public int target_account_id { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount(INR)")]  
        public int Transfer_amount { get; set; }

        public int bal1 { get; set; }
        public int bal2 { get; set; }


        public DataSet StoreAllData { get; set; }
    }

}