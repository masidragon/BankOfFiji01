using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BankOfFiji01.Models
{
    public class BillPaymentRepository
    {
        //private BankOfFijiEntities db;

        //public BillPaymentRepository()
        //{
        //    db = new BankOfFijiEntities();
        //}

        public static async Task<int> CheckBankAccountCount(int info)
        {
            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(info);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/accscount", httpContent);

            var jsonString = await response.Content.ReadAsStringAsync();

            try
            {
                int AccountCount = Convert.ToInt32(jsonString);
                return AccountCount;
            }
            catch
            {
                int AccountCount = 0;
                return AccountCount;
            }
        }

        public static async Task<List<Account>> CheckBankAccountNumbers(int info)
        {
            var ListContent = new List<BankAccount>();

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(info);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/checkaccs?custid=" + info, httpContent);


            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<List<Account>>(jsonString);

            return accounts;
        }

        //public IList<BankAccount> MyAccounts()
        //{
        //    int CustIDHandler = Convert.ToInt32(HttpContext.Current.Session["CustID"]);

        //    var content = new List<BankAccount>();

        //    var Acc = from item in db.BankAccount
        //              where item.Users.userId == CustIDHandler
        //              select item;

        //    foreach (var item in Acc)
        //    {
        //        content.Add(item);
        //    }

        //    return content;
        //}

        public static async Task<List<Account>> GetOtherAccounts(int info)
        {
            int CustIDHandler = Convert.ToInt32(HttpContext.Current.Session["CustID"]);

            var ListContent = new List<Account>();

            Account NewQuery = new Account();

            NewQuery.UserID = CustIDHandler;
            NewQuery.ID = info;

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(NewQuery);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/getotheracc", httpContent);

            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<List<Account>>(jsonString);

            return accounts;
        }

        public static async Task<List<Account>> GetCompanyAccounts()
        {
            int CustIDHandler = Convert.ToInt32(HttpContext.Current.Session["CustID"]);

            var ListContent = new List<Account>();

            Account NewQuery = new Account();

            NewQuery.UserID = CustIDHandler;
            NewQuery.ID = 0;

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(NewQuery);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/getcompanyaccs", httpContent);

            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<List<Account>>(jsonString);

            return accounts;
        }


        public static async Task<List<Intervals>> GetTimeperiod()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:55303/getintervals");

            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<List<Intervals>>(jsonString);

            return accounts;
        }


        public static async Task<string> EnableTransfer(TransferViewModel info)
        {
            Transfer NewQuery = new Transfer();

            NewQuery.Acc_ID = info.Acc_ID;
            NewQuery.Transac_Type_ID = info.Transac_Type_ID;
            NewQuery.TransferAcc_ID = info.TransferAcc_ID;
            NewQuery.Trans_Amount = info.Trans_Amount;
            NewQuery.StartDate = info.startDate;
            NewQuery.EndDate = info.endDate;
            NewQuery.Interval = info.Period;



            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(NewQuery);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/transfertoacc", httpContent);

            var responseString = await response.Content.ReadAsStringAsync();

            string Trim = responseString.Trim('"');

            return Trim;
        }
    }
}