using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace RetailBankManagement.Models
{
    public class AccountSearch
    {
        private int t_id;
        private string descript;
        private DateTime Date;
        private int amount;
        private int a_id;
        private int c_id;
        private string acc_type;
        private string status;
        private string message;
        private DateTime up_date;
        public string account_status { get; set; }
        public string account_message { get; set; }
        public DateTime update_date { get; set; }

        public AccountSearch(int t_id, string descript, DateTime Date, int amount)
        {
            // TODO: Complete member initialization
            TransactionID = t_id;
            TransactionDescription = descript;
            TransactionDate = Date;
            TransactionAmount = amount;
        }
        [Required(ErrorMessage = "Please enter Customer ID or Customer SSN ID")]
        [Display(Name = "Customer ID or Account ID")]
        [Range(100000000, 999999999, ErrorMessage = "Customer ID or Account ID must be a 9 digit number")]
        public int Customer_Id { get; set; }

        [Required(ErrorMessage = "Please select search mode")]
        [Display(Name = "Search")]
        public string Account_Search { get; set; }

        public DataSet AccountDATA { get; set; }

        [Required(ErrorMessage = "Please enter account type")]
        [Display(Name = "AccountType")]
        [StringLength(7, ErrorMessage = "Please enter valid account type")]
        public String AccountType { get; set; }

        [Required(ErrorMessage = "Please enter AccountID")]
        [Display(Name = "Account ID")]
        [Range(100000000, 999999999, ErrorMessage = " Account ID must be a 9 digit number")]
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Please enter Customer ID ")]
        [Display(Name = "Customer ID")]
        [Range(100000000, 999999999, ErrorMessage = "Customer ID must be a 9 digit number")]
        public int CustomerID { get; set; }


        [Required(ErrorMessage = "Please enter deposit ammount ")]
        [Display(Name = "Deposit(INR)")]
        //[Range(100000000, 999999999, ErrorMessage = "Customer ID must be a 9 digit number")]
        public int Deposit { get; set; }

        [Required(ErrorMessage = "Please enter deposit Amount")]
        [Display(Name = "Deposit Amount(INR)")]
        public int DepositAmount { get; set; }

        [Required(ErrorMessage = "Please enter balance Amount")]
        [Display(Name = "Balance Amount(INR)")]
        public int BalanceAmount { get; set; }

        [Required(ErrorMessage = "Please enter withdraw amount")]
        [Display(Name = "Withdraw Amount(INR)")]
        public int withdrawamount { get; set; }
        
        [Required(ErrorMessage = "Please select no. of transaction")]
        [Display(Name = "No. of transaction")]
        public string transaction_no { get; set; }

        public int TransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionAmount { get; set; }
        public string TransactionDescription { get; set; } 

        public AccountSearch()
        {
        }
        public DataSet storeData { get; set; }

        public AccountSearch(int a_id, int c_id, string acc_type, string status, string message, DateTime up_date)
        {
            // TODO: Complete member initialization
            AccountID = a_id;
            Customer_Id = c_id;
            AccountType = acc_type;
            account_status = status;
            account_message = message;
            update_date = up_date;
        }


    }
}
