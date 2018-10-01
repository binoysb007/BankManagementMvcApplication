using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using RetailBankManagement.Models;
using RetailBankManagement.DataAccessLayer;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Helpers;
using System.IO;
using System.Web.UI;

namespace RetailBankManagement.Controllers
{
    public class CustomerController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer cu)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();
                    int result = obj.addCustomer(cu);
                    ViewData["Result"] = result;
                    TempData["AlertMessage"] = "Customer successfully created with "+result+" Customer Id";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Error in Saving Data");
                    return View();
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Exception");
            }
        }
        

        public ActionResult ViewCustomerByCustomerId()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ViewCustomerByCustomerId(Customer cu)
        {
            if (!ModelState.IsValid)
            {
                DataAccessLayer.DBManager obj=new DataAccessLayer.DBManager();

                int temp_id = cu.temp_ID;
                string temp_search = cu.Customer_Search;
                int check = obj.checkCustomer(temp_id, temp_search);
                if (check > 0)
                {
                    return RedirectToAction("viewCustbyID", new { data = temp_id, mode = temp_search });
                }
                else
                {
                    if (Request.HttpMethod == "POST")
                    {
                        TempData["AlertMessage"] = "Requested customer ID is not present in system.";
                        ModelState.Clear();
                    }
                    
                    return View();
                }

                
            }
            return View();

        }

        public ActionResult viewCustbyID(int data,string mode)
        {
            
                DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();
                Customer cus = new Customer();
                cus.storeData = obj.viewCustomerbyID(data, mode);
          
                  return View(cus);
       
            
       
        }

        public ActionResult DeleteCustomer(int id)
        {
            DataAccessLayer.DBManager objDB = new DataAccessLayer.DBManager();
            DataSet ds = objDB.viewCustomerbyID(id, "ByCustomerID");
            Customer MB = new Customer();
            MB.Customer_ssn_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Customer_SSN_ID"].ToString());
            MB.Customer_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Customer_ID"].ToString());
            MB.Customer_Name = ds.Tables[0].Rows[0]["Customer_Name"].ToString();
            MB.Customer_Age = Convert.ToInt32(ds.Tables[0].Rows[0]["Customer_Age"].ToString());
            MB.Customer_Address = ds.Tables[0].Rows[0]["Customer_Address"].ToString();
            return View(MB);
        }
        [HttpPost]
        public ActionResult DeleteCustomer(Customer MB)
        {
            DataAccessLayer.DBManager objDB = new DataAccessLayer.DBManager();
            int result = objDB.deleteCustomer(MB);
            if (result == 0)
            {
                return RedirectToAction("Exception");
            }
            ViewData["resultDelete"] = result;
            return View();
          

        }

        public ActionResult updatecustomer(int id)
        {
            DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();
            DataSet ds = obj.viewCustomerbyID(id, "ByCustomerID");
            Customer MB = new Customer();
            MB.Customer_ssn_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Customer_SSN_ID"].ToString());
            MB.Customer_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Customer_ID"].ToString());
            MB.Customer_Name = ds.Tables[0].Rows[0]["Customer_Name"].ToString();
            MB.Customer_Age = Convert.ToInt32(ds.Tables[0].Rows[0]["Customer_Age"].ToString());
            MB.Customer_Address = ds.Tables[0].Rows[0]["Customer_Address"].ToString();
            MB.Customer_State = ds.Tables[0].Rows[0]["Customer_State"].ToString();
            MB.Customer_City = ds.Tables[0].Rows[0]["Customer_City"].ToString();
            return View(MB);
        }

        [HttpPost]
        public ActionResult updatecustomer(Customer customer)
        {
            DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();
            string result= obj.UpdateCustomer(customer);
            ViewData["resultupdate"] = result;
            TempData["notice"] = "Customer update initiated successfully.";
            return View();
          
        }
        public ActionResult ViewAllCustomer()
        {
            DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();
            List<Customer> clist = obj.ViewAllCustomer();
            return View(clist);
        }

        public ActionResult Exception()
        {
            return View();
        }

        public ActionResult ViewCustomerforTeller()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ViewCustomerforTeller(Customer cu)
        {
            if (!ModelState.IsValid)
            {
                DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();

                int temp_id = cu.temp_ID;
                string temp_search = cu.Customer_Search;
                int check = obj.checkCustomer(temp_id, temp_search);
                if (check > 0)
                {
                    return RedirectToAction("ViewCustomerteller", new { data = temp_id, mode = temp_search });
                }
                else
                {
                    if (Request.HttpMethod == "POST")
                    {
                        TempData["AlertMessage"] = "Requested customer ID is not present in system.";
                        ModelState.Clear();
                    }

                    return View();
                }


            }
            return View();
        }

        public ActionResult ViewCustomerteller(int data, string mode)
        {
            DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();
            Customer cus = new Customer();
            Session["data"] = data;
            Session["mode"] = mode;
            cus.storeData = obj.viewCustomerbyID(data, mode);
            return View(cus);
        }
       public void DownloadExcel()
        {
            DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();
            List<Customer> clist = obj.ViewAllCustomer();

            WebGrid grid = new WebGrid(source:clist);

            string gridData = grid.GetHtml(columns: grid.Columns(
                            grid.Column("Customer_Id", "Customer ID"),
                            grid.Column("Customer_ssn_Id", "Customer SSN ID"),
                            grid.Column("customer_status", "Status"),
                            grid.Column("customer_message", "Message"),
                            grid.Column("update_date", "Last updated")
                            )).ToString();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=customerexcel.xls");
            Response.ContentType="application/excel";
            Response.Write(gridData);
            Response.End();

        }
        public void DownloadWordCustomerDetails()
        {
            DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();
            Customer cus = new Customer();
            cus.storeData = obj.viewCustomerbyID(Convert.ToInt32(Session["data"]), Session["mode"].ToString());
            GridView gv = new GridView();
            gv.DataSource = cus.storeData;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=customerdetails.doc");
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