using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankOfFiji01.Models;
using System.Threading.Tasks;
using BankOfFiji01.Repositories;
using System.Web.Routing;

namespace BankOfFiji01.Controllers
{
    public class AccountController : Controller
    {
        //private BankOfFijiEntities db = new BankOfFijiEntities();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Login info)
        {
            var ValidationResult = await LoginRepo.ValidateUser(info);

            if (ValidationResult != "Login Success")
            {
                TempData["Error"] = String.Concat("Dear valued customer, ", ValidationResult);
                return RedirectToAction("Login");
            }

            // Request details only of validation passed
            try
            {
                var UserDetail = await LoginRepo.RetreiveIDs(info);

                Session["CustID"] = UserDetail.CustomerID;
                Session["Role"] = UserDetail.RoleID;
                Session["Username"] = info.Username;

                if(UserDetail.RoleID == 1002)
                {
                    return RedirectToAction("Admin", "Dashboard");
                }

                return RedirectToAction("Home", "Dashboard");
            }
            catch
            {
                ModelState.AddModelError("", "Invalid Credentials");
            }

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(UserAccounts objUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var objQuery = from Object in db.UserAccounts
        //                           where objUser.UserName == Object.UserName && objUser.User_Password == Object.User_Password
        //                           select Object;

        //            var obj = objQuery.FirstOrDefault();
        //            Session["CustID"] = obj.Cust_ID;
        //            Session["Role"] = obj.Role_ID;
        //            Session["UserName"] = obj.UserName;

        //            return RedirectToAction("Dashboard", "Home");
        //        }
        //        catch
        //        {
        //            var username = from Object in db.UserAccounts
        //                           where objUser.UserName == Object.UserName
        //                           select Object;

        //            if (!username.Any())
        //            {
        //                // User exists
        //                TempData["Error"] = "Dear valued customer, Please enter the correct username!";
        //            }
        //            else
        //            {
        //                // No user exists
        //                TempData["Error"] = "Dear valued customer, Please enter the correct password!";
        //            }

        //            return RedirectToAction("Login");
        //        }

        //    }
        //    return View(objUser);
        //}
    }
}