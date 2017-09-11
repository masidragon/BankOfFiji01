using BankOfFiji01.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BankOfFiji01.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public async Task<ActionResult> Home()
        {
            int CustIDHandler = Convert.ToInt32(Session["CustID"]);
            var UserDetail = await DashboardRepo.FindUserDetails(CustIDHandler);

            return View(UserDetail);
        }
    }
}