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

            foreach (var result in Notifications)
            {
                NotificationsViewModel newentry = new NotificationsViewModel();
                newentry.NotificationID = result.NotificationID;
                newentry.NotificationType = result.NotificationType;

                if(result.NotificationStatus == "Deny")
                {
                    newentry.NotificationStatus.Add(new SelectListItem()
                    {
                        Text = "Deny",
                        Value = "Deny"
                    });
                    newentry.NotificationStatus.Add(new SelectListItem()
                    {
                        Text = "Allow",
                        Value = "Allow"
                    });
                }
                else
                {
                    newentry.NotificationStatus.Add(new SelectListItem()
                    {
                        Text = "Allow",
                        Value = "Allow"
                    });
                    newentry.NotificationStatus.Add(new SelectListItem()
                    {
                        Text = "Deny",
                        Value = "Deny"
                    });
                }


                CreateVM.Add(newentry);
            }

            return View(CreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Notifications(NotificationsViewModel entry)
        {

            return View();
        }
    }
}