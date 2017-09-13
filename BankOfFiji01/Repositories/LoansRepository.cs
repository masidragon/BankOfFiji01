using BankOfFiji01.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace BankOfFiji01.Repositories
{
    public class LoansRepository
    {
        public static async Task<List<Account>> GetLoanTypes()
        {
            int CustIDHandler = Convert.ToInt32(HttpContext.Current.Session["CustID"]);

            var ListContent = new List<Account>();

            Account NewQuery = new Account();

            NewQuery.UserID = CustIDHandler;
            NewQuery.ID = 0;

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(NewQuery);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/loantype", httpContent);

            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<List<Account>>(jsonString);

            return accounts;
        }


        public static async Task<List<Account>> GetAssets()
        {
            int CustIDHandler = Convert.ToInt32(HttpContext.Current.Session["CustID"]);

            var ListContent = new List<Account>();

            Account NewQuery = new Account();

            NewQuery.UserID = CustIDHandler;
            NewQuery.ID = 0;

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(NewQuery);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/assettypes", httpContent);

            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<List<Account>>(jsonString);

            return accounts;
        }

        public static async Task<string> sendloanapplication(LoanViewModel info)
        {
            Loan loan = new Loan();
            loan.CustID = info.CustID;
            loan.LoanID = info.LoanID;
            loan.LoanAmount = info.LoanAmount;
            loan.AssetID = info.AssetID;
            loan.AccountNo = info.AccountNo;

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(loan);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/loanapplication", httpContent);

            var responseString = await response.Content.ReadAsStringAsync();

            string Trim = responseString.Trim('"');

            return Trim;
        }
    }
}