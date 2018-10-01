using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailBankManagement.Models;
using System.Data;
using RetailBankManagement.Controllers.CreateCustomerAccounts2porDataAccessLayer;
using RetailBankManagement.DataAccessLayer;



namespace RetailBankManagement.Controllers
{
    public class CreateCustomerAccounts2porController : Controller
    {
       
        public ActionResult InsertCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertCustomer(CreateCustomerAccounts2por meth)
        {

            if (ModelState.IsValid)
            {
                DataAccessLayer.DBManager obj = new DataAccessLayer.DBManager();
                int check = obj.checkCustomer(meth.CustomerID, "ByCustomerID");
                if (check > 0)
                {
                    CreateCustomerAccounts2porDbData ab = new CreateCustomerAccounts2porDbData();
                    int result = ab.insertdata(meth);
                    if (result == 0)
                    {
                        return RedirectToAction("Exception");
                    }
                    ViewData["Result"] = result;
                    TempData["AlertMessage"] = "Account is created successfully with " + result + " AccountID";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    TempData["AlertMessage"] = "Requested customer ID is not present in system.";
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        public ActionResult Exception()
        {
            return View();
        }
    }
}
