using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailBankManagement.Models;
using RetailBankManagement.DAL;

namespace RetailBankManagement.Controllers
{
    public class WebsiteLoginController : Controller
    {
       
        // GET: WebsiteLogin
        public ActionResult WebsiteLogin()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult WebsiteLogin(WebsiteLoginModels MD)
        {
            if (ModelState.IsValid)
            {
                DAL.WebsiteLogin LoginObj = new DAL.WebsiteLogin();
                string role = LoginObj.Login(MD);
                ModelState.Clear(); //clearing model
                if (role.Equals("Executive"))
                {
                    Session["user_id"] = MD.User_ID;
                    Session["Password"] = MD.Password;
                    return RedirectToAction("LoginforExecutive", "WebsiteLogin");
                }
                else if (role.Equals("Cashier"))
                {
                    Session["user_id"] = MD.User_ID;
                    Session["Password"] = MD.Password;
                    return RedirectToAction("Loginforteller", "WebsiteLogin");
                }
                else
                {
                    if (Request.HttpMethod == "POST")
                    {
                        TempData["AlertMessage"] = "Invalid username or password.";
                        ModelState.Clear();
                    }

                    return View();
                }
            }
            //else
            //{
            //    TempData["AlertMessage"] = "Invalid Username or Password";
            return View();
            //}
            
        }
        public ActionResult LoginforExecutive()
        {
            return View();
        }

        public ActionResult Loginforteller()
        {
            return View();
        }
    }
}