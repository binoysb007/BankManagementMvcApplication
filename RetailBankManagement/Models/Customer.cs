using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RetailBankManagement.Models
{
    public class Customer
    {
        private int c_id;
        private int ssn_id;
        private string status;
        private string message;
        private DateTime up_date;

        public Customer(int c_id, int ssn_id, string status, string message, DateTime up_date)
        {
            // TODO: Complete member initialization
            Customer_Id = c_id;
            Customer_ssn_Id = ssn_id;
            customer_status = status;
            customer_message = message;
            update_date = up_date;
        }
        public string customer_status { get; set; }
        public string customer_message { get; set; }
        public DateTime update_date { get; set; }

        public Customer(){}

        [Required(ErrorMessage = "Please enter customer ID or customer SSN ID")]
        [Display(Name = "Customer ID or Customer SSN ID")]
        [Range(100000000, 999999999, ErrorMessage = "customer ID or customer SSN ID must be a 9 digit number")]
        public int temp_ID { get; set; }

        [Required(ErrorMessage = "Please enter customer ID")]
        [Display(Name = "Customer ID")]
        [Range(100000000, 999999999, ErrorMessage = "customer ID must be a 9 digit number")]
        public int Customer_Id { get; set; }
       
        [Required(ErrorMessage = "Please enter unique SSN ID")]
        [Display(Name = "Customer SSN ID")]
        [Range(100000000, 999999999, ErrorMessage = "customer SSN ID must be a 9 digit number")]
        public int Customer_ssn_Id { get; set; }


        [Required(ErrorMessage = "Please enter customer name")]
        [Display(Name = "Customer Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "customer name must be between 3 and 100 characters!")]
        [RegularExpression(@"^(([A-Za-z]+[\s]{1}[A-Za-z]+)|([A-Za-z]+))$", ErrorMessage="Invalid characters in customer name")]
        public string Customer_Name { get; set; }

        [Required(ErrorMessage = "Please enter age")]
        [Display(Name = "Age(in years)")]
        [Range(10,150, ErrorMessage = "Please enter valid age between 10 to 150 years")]
        public int Customer_Age { get; set; }

        [Required(ErrorMessage = "Please enter customer address")]
        [Display(Name = "Address")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Customer address must be between 3 and 100 characters!")]
        [RegularExpression(@"^(?![0-9]*$)[a-zA-Z0-9]+$", ErrorMessage = "Customer address must be alphabetic or alphanumeric")]
        public string Customer_Address { get; set; }

        [Required(ErrorMessage = "Please select state")]
        [Display(Name = "State")]
        public string Customer_State { get; set; }

        [Required(ErrorMessage = "Please select city")]
        [Display(Name = "City")]
        public string Customer_City { get; set; }
        
        [Required(ErrorMessage="Please select search mode")]
        [Display(Name="Search")]
        public string Customer_Search { get; set; }

        [Display(Name = "DOB")]
        public DateTime DOB { get; set; }

        public DataSet storeData { get; set; }
    }
    
    
    
}