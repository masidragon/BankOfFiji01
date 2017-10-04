using BankOfFiji01.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BankOfFiji01.Repositories
{
    public class DashboardRepo
    {
        /// <summary>
        /// Return a summary of accounts owned by logged in user as well as their personal details
        /// </summary>
        /// <param name="CustID"> The Customer ID to assist in the querying of information </param>
        /// <returns> A object containing all customer details and a list object of all accounts owned by customer</returns>
        public static async Task<UserDetails> FindUserDetails(int CustID)
        {
            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(CustID);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.GetAsync("http://localhost:55303/userdetails?CustID=" + CustID);

            UserDetails responseString = null;
            var jsonString = await response.Content.ReadAsStringAsync();
            responseString = JsonConvert.DeserializeObject<UserDetails>(jsonString);
            return responseString;
        }

        public static async Task<DataTable> getpdf()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:55303/interestpdf");

            DataTable responseString = null;
            var jsonString = await response.Content.ReadAsStringAsync();
            responseString = JsonConvert.DeserializeObject<DataTable>(jsonString);
            return responseString;
        }

        public static async Task<List<Notification>> GetNotifications()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:55303/notifications");

            List<Notification> responseString = null;
            var jsonString = await response.Content.ReadAsStringAsync();
            responseString = JsonConvert.DeserializeObject<List<Notification>>(jsonString);
            return responseString;
        }

        public static async Task<Notification> GetSingleNotifications(int info)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:55303/getsinglenotifications?info=" + info);

            Notification responseString = null;
            var jsonString = await response.Content.ReadAsStringAsync();
            responseString = JsonConvert.DeserializeObject <Notification> (jsonString);
            return responseString;
        }

        public static async Task<string> SetSingleNotifications(Notification info)
        {
            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(info);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/setsinglenotifications", httpContent);

            string responseString = null;
            var jsonString = await response.Content.ReadAsStringAsync();
            responseString = JsonConvert.DeserializeObject<string>(jsonString);
            return responseString;
        }
    }
}