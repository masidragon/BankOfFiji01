using BankOfFiji01.Models;
using BankOfFiji01.Repositories;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public async Task<ActionResult> Admin()
        {
            int CustIDHandler = Convert.ToInt32(Session["CustID"]);
            var UserDetail = await DashboardRepo.FindUserDetails(CustIDHandler);

            return View(UserDetail);
        }

        public async Task<ActionResult> Notifications()
        {
            List<NotificationsViewModel> CreateVM = new List<NotificationsViewModel>();
            
            var Notifications = await DashboardRepo.GetNotifications();

            return View(Notifications);
        }

        public async Task<ActionResult> EditNotification(int id)
        {
            Notification notification = await DashboardRepo.GetSingleNotifications(id);

            if (notification == null)
            {
                return HttpNotFound();
            }

            return View(notification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditNotification(Notification entry)
        {
            var content = await DashboardRepo.SetSingleNotifications(entry);

            if (content[0] == 'Y')
            {
                TempData["Success"] = content;
                return RedirectToAction("Notifications");
            }

            TempData["Error"] = content;
            return RedirectToAction("Notifications");
        }
    }
}