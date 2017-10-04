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
    public class TransactiontRepository
    {
        public static async Task<List<TransactionHistory>> GetStatement(TransactionRequest info)
        {
            var ListContent = new List<TransactionHistory>();

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(info);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/getstatement", httpContent);


            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<List<TransactionHistory>>(jsonString);

            return accounts;
        }

        public static async Task<DataTable> GetNetIncome(TransactionRequest info)
        {
            var ListContent = new List<TransactionHistory>();

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(info);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/interestpdf", httpContent);


            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<DataTable>(jsonString);

            return accounts;
        }
    }
}