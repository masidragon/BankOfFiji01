using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string NotificationType { get; set; }
        public string NotificationStatus { get; set; }
    }
}