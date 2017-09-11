using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankOfFiji01.Models;

namespace BankOfFiji01.Controllers
{
    public class HomeController : Controller
    {
        private BankOfFijiEntities db = new BankOfFijiEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnotherLink()
        {
            return View();
        }

        public ActionResult Dashboard(UserDetails info)
        {
            int CustIDHandler = Convert.ToInt32(Session["CustID"]);
            return View(info);
        }

        public ActionResult Login()
        {
            return View("Login");
        }
    }
}