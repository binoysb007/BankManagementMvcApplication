using RetailBankManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailBankManagement.DataAccessLayer;
using RetailBankManagement.Controllers.CreateCustomerAccounts2porDataAccessLayer;
using System.Data;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace RetailBankManagement.Controllers
{
    public class AccountSearchController : Controller
    {
        // GET: AccountSearch
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountByCustomerId()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AccountByCustomerId(AccountSearch cu)
        {
            if (!ModelState.IsValid)
            {
                CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
                int temp_id = cu.Customer_Id;
                string temp_search = cu.Account_Search;
                int check = obj.checkAccount(temp_id, temp_search);
                if (check > 0)
                {
                    return RedirectToAction("AccountById", new { data = temp_id, mode = temp_search });
                }
                else
                {
                    if (Request.HttpMethod == "POST")
                    {
                        TempData["AlertMessage"] = "Requested account is not present in the system.";
                        ModelState.Clear();
                    }
                    return View();
                }
            }
                return View();
        }

        public ActionResult AccountById(int data, string mode)
        {
          
            CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
         
            AccountSearch acc = new AccountSearch();
            Session["acc_data"] = data;
            Session["acc_mode"] = mode;
            acc.AccountDATA = obj.AccountSearch(data, mode);
            return View(acc);
        }


        public ActionResult DeleteAccount(int id, int accountID)
        {
            CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
            DataSet ds = obj.AccountSearch(id, "ByCustomerID");
            AccountSearch MB = new AccountSearch();
                //MB.AccountID = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountID"].ToString());
            MB.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"].ToString());
            MB.AccountType = ds.Tables[0].Rows[0]["AccountType"].ToString();
            MB.DepositAmount = Convert.ToInt32(ds.Tables[0].Rows[0]["DepositAmount"].ToString());
            MB.AccountID = accountID;
           
            return View(MB);
        }
        [HttpPost]
        public ActionResult DeleteAccount(AccountSearch MB)
        {
            int c_id = Convert.ToInt32(MB.CustomerID);
            int bal = Convert.ToInt32(MB.DepositAmount);
            int a_id = Convert.ToInt32(MB.AccountID);
            CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
            int result = obj.DeleteAccount(c_id,bal,a_id);
            if (result == 0)
            {
                return RedirectToAction("Exception");
            }
            ViewData["resultDelete"] = result;
            return View();
          

        }
        public ActionResult Withdraw(int id)
        {
            CreateCustomerAccounts2porDbData objDB = new CreateCustomerAccounts2porDbData();
            DataSet ds = objDB.AccountSearch(id,"ByAccountID");
            AccountSearch PD = new AccountSearch();
            PD.AccountID = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountID"].ToString());
            PD.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"].ToString());
            PD.AccountType = ds.Tables[0].Rows[0]["AccountType"].ToString();
            PD.BalanceAmount = Convert.ToInt32(ds.Tables[0].Rows[0]["DepositAmount"].ToString());

            return View("Withdraw", PD);
          
        }
        [HttpPost]
        public ActionResult Withdraw(AccountSearch PD)
        {
            if (!(PD.withdrawamount == 0 || PD.BalanceAmount == 0||PD.withdrawamount<0))
            {

            if (PD.withdrawamount <= PD.BalanceAmount)
            {
                ViewBag.accid = PD.AccountID;
                ViewBag.BalBeforwith = PD.BalanceAmount;
                int latbalance = PD.BalanceAmount - PD.withdrawamount;
                CreateCustomerAccounts2porDbData objad = new CreateCustomerAccounts2porDbData();
                int mi = objad.withdrawamount(PD, latbalance);
                if (mi > 0)
                {
                    if (Request.HttpMethod == "POST")
                    {
                        TempData["AlertMessage"] = "Amount INR " + PD.withdrawamount + " withdrawn successfull.";
                    }
                    ViewBag.latbalance = latbalance;
                    return View("AfterWithdraw");
                }
                else
                {
                    return RedirectToAction("Exception");
                }
            }
            else
            {
                if (Request.HttpMethod == "POST")
                {
                    TempData["AlertMessage"] = "Withdraw is not allowed choose a amount smaller than balance amount.";
                }

                return View("Withdraw", PD);
            }
        }
            else
            {
                if (PD.withdrawamount == 0||PD.withdrawamount<0)
                {
                    TempData["AlertMessage"] = "Please enter some valid amount to withdraw.";
                }
                else if (PD.BalanceAmount == 0)
                {
                    TempData["AlertMessage"] = "Please deposit some amount.";
                }
                return View("Withdraw", PD);
            }
        }

        public ActionResult Deposit(int id)
        {

            CreateCustomerAccounts2porDbData objDB = new CreateCustomerAccounts2porDbData();
            DataSet ds = objDB.AccountSearch(id, "ByAccountID");
            AccountSearch PD = new AccountSearch();
            PD.AccountID = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountID"].ToString());
            PD.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"].ToString());
            PD.AccountType = ds.Tables[0].Rows[0]["AccountType"].ToString();
            PD.BalanceAmount = Convert.ToInt32(ds.Tables[0].Rows[0]["DepositAmount"].ToString());

            return View("Deposit", PD);
            
        }
        [HttpPost]
        public ActionResult Deposit(AccountSearch PD)
        {
            if (PD.DepositAmount == 0 ||PD.DepositAmount<0)
            {
                TempData["AlertMessage"] = "Please enter some valid amount to deposit.";
                return View("Deposit", PD);
            }
            else 
            {
                ViewBag.accid = PD.AccountID;
                ViewBag.BalBeforwith = PD.BalanceAmount;
                int latbalance = PD.BalanceAmount + PD.DepositAmount;
                CreateCustomerAccounts2porDbData objad = new CreateCustomerAccounts2porDbData();
                int mi = objad.depositamount(PD, latbalance);
                if (mi > 0)
                {
                    if (Request.HttpMethod == "POST")
                    {
                        TempData["AlertMessage"] = "Amount INR " + PD.DepositAmount + " deposited successfully.";
                    }
                    ViewBag.latbalance = latbalance;
                    return View("AfterDeposit");
                }
                else
                {
                    return RedirectToAction("Exception");
                }
            }
            }


        public ActionResult transfer(int id)
        {
            CreateCustomerAccounts2porDbData objDB = new CreateCustomerAccounts2porDbData();
            DataSet ds = objDB.AccountSearch(id, "ByAccountID");
            transfer_amount PD = new transfer_amount();
            PD.source_account_id = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountID"].ToString());
            return View(PD);
        }

        [HttpPost]
        public ActionResult transfer(transfer_amount tu)
        {
            if (ModelState.IsValid) // Check the model state for any validation errors
            {
                List<int> balist = new List<int>();
                CreateCustomerAccounts2porDbData cu = new CreateCustomerAccounts2porDbData();
                if (tu.source_account_id != tu.target_account_id)
                {
                    int bal1 = cu.viewbalance(tu.source_account_id);
                    int bal2 = cu.viewbalance(tu.target_account_id);
                    if (bal1 < 0 || bal2 < 0)
                    {
                        return RedirectToAction("Exeption");
                    }
                    balist.Add(bal1);
                    balist.Add(bal2);
                    if (tu.Transfer_amount > 0)
                    {
                        if (bal1 >= tu.Transfer_amount)
                        {
                            int a = bal1;
                            int b = bal2;
                            int c = tu.Transfer_amount;
                            a = a - c;
                            b = b + c;
                            balist.Add(a);
                            balist.Add(b);
                            int m = cu.transfer_amount(balist, tu);
                            int firstaccountupdate = balist.ElementAt(2);
                            int secondaccountupdate = balist.ElementAt(3);
                            if (m != 0)
                            {
                                if (m == 1)
                                {
                                    ViewBag.acc1 = tu.source_account_id;
                                    ViewBag.acc2 = tu.target_account_id;
                                    ViewBag.balbefupdateacc1 = bal1;
                                    ViewBag.balbefupdateacc2 = bal2;
                                    ViewBag.balaftupdateacc1 = firstaccountupdate;
                                    ViewBag.balaftupdateacc2 = secondaccountupdate;
                                    ViewBag.amount = tu.Transfer_amount;
                                    if (Request.HttpMethod == "POST")
                                    {
                                        TempData["AlertMessage"] = "Amount INR "+tu.Transfer_amount+" has been transferred succesfully.";
                                    }
                                    return View("aftertransfer");
                                }
                                else if (m == 2)
                                {
                                    TempData["AlertMessage"] = "Please enter existing target account ID.";
                                }
                            }
                            else if (m == 0)
                            {
                                return RedirectToAction("Exception");
                            }

                        }

                        else
                        {
                            if (Request.HttpMethod == "POST")
                            {
                                string sp = "Amount should be less than or equal to " + bal1 + " Rs.";
                                TempData["AlertMessage"] = sp;
                            }
                            return View();


                        }
                    }
                    else
                    {
                        TempData["AlertMessage"] = "Please enter some valid amount to transfer.";
                    }

                    return View();

                }
                else
                {
                    TempData["AlertMessage"] = "Target account ID can not be same as source account ID.";
                }
                return View();
            }
            else
            {
                return View();
            }
        }

       

        public ActionResult AccountStatement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AccountStatement(AccountSearch acc)
        {
            if (!ModelState.IsValid) 
            {
                CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
                int temp_id = acc.AccountID;
                int check = obj.checkAccount(temp_id, "ByAccountID");
                if (check > 0)
                {
                    int cycle = Convert.ToInt32(acc.transaction_no);
                    return RedirectToAction("viewAccountStatement", new { Temp_id = temp_id, Cycle = cycle });
                }
                else
                {
                    if (Request.HttpMethod == "POST")
                    {
                        TempData["AlertMessage"] = "Requested account is not present in the system.";
                        ModelState.Clear();
                    }
                    return View();

                }
            }
            return View();
            
        }

        public ActionResult viewAccountStatement(int Temp_id,int Cycle)
        {
            CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
            List<AccountSearch> list = obj.viewAccountStatement(Temp_id, Cycle);
            Session["id"] = Temp_id;
            Session["Cycle"] = Cycle;

            if (list==null)
            {
                return RedirectToAction("Exception");
            }
            return View(list);    
        }


        public ActionResult ViewAllAccount()
        {
            CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
            List<AccountSearch> clist = obj.viewALLAccount();
            return View(clist);
        }

        public ActionResult Exception()
        {
            return View();
        }


        public ActionResult AccountsearchforExecutive()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AccountsearchforExecutive(AccountSearch cu)
        {
            if (!ModelState.IsValid)
            {
                CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
                int temp_id = cu.Customer_Id;
                string temp_search = cu.Account_Search;
                int check = obj.checkAccount(temp_id, temp_search);
                if (check > 0)
                {
                    return RedirectToAction("AccountsearchExecutive", new { data = temp_id, mode = temp_search });
                }
                else
                {
                    if (Request.HttpMethod == "POST")
                    {
                        TempData["AlertMessage"] = "Requested account is not present in system.";
                        ModelState.Clear();
                    }
                    return View();
                }
            }
            return View();
        }

        public ActionResult AccountsearchExecutive(int data, string mode)
        {
          
            CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
         
            AccountSearch acc = new AccountSearch();
            acc.AccountDATA = obj.AccountSearch(data, mode);
            return View(acc);
        }
        public void DownloadExcelAllAccount()
        {
            CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
            List<AccountSearch> clist = obj.viewALLAccount();

            WebGrid grid = new WebGrid(source: clist);

            string gridData = grid.GetHtml(columns: grid.Columns(
                          grid.Column("AccountID", "Account ID"),
                                grid.Column("Customer_Id", "Customer ID"),
                                grid.Column("AccountType", "Account Type"),
                                grid.Column("account_status", "Status"),
                                grid.Column("account_message", "Message"),
                                grid.Column("update_date", "Last Updated(mm-dd-yyyy)")
                            )).ToString();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=accountexcel.xls");
            Response.ContentType = "application/excel";
            Response.Write(gridData);
            Response.End();

        }
        public void DownloadExcelAccountStatement()
        {
            CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();
            List<AccountSearch> list = obj.viewAccountStatement(Convert.ToInt32(Session["id"]), Convert.ToInt32(Session["Cycle"]));

            WebGrid grid = new WebGrid(source: list);

            string gridData = grid.GetHtml(columns: grid.Columns(
                          grid.Column("TransactionID", "Transaction ID"),
                grid.Column("TransactionDescription", "Description"),
                grid.Column("TransactionDate", "Date(mm-dd-yyyy)"),
                grid.Column("TransactionAmount", "Amount(INR)")
                            )).ToString();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=AccountStatementexcel.xls");
            Response.ContentType = "application/excel";
            Response.Write(gridData);
            Response.End();

        }
        public void DownloadWordAccountDetails()
        {
            CreateCustomerAccounts2porDbData obj = new CreateCustomerAccounts2porDbData();

            AccountSearch acc = new AccountSearch();
            acc.AccountDATA = obj.AccountSearch(Convert.ToInt32(Session["acc_data"]), Session["acc_mode"].ToString());
            GridView gv = new GridView();
            gv.DataSource = acc.AccountDATA;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=accountdetails.doc");
            //Response.ContentType = "application/vnd.ms-word ";
            //Response.Write(gv);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gv.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }

    }
}