using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOfFiji01.Models
{
    public class NotificationsViewModel
    {

        public NotificationsViewModel()
        {
            NotificationStatus = new List<SelectListItem>();
        }

        public int NotificationID { get; set; }
        public string NotificationType { get; set; }
        public IList<SelectListItem> NotificationStatus { get; set; }
    }

}