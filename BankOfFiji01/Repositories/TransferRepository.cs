﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BankOfFiji01.Models
{
    public class TransferRepository
    {
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

        public static async Task<Transfer> EnableTransfer(TransferViewModel info)
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

            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<Transfer>(jsonString);

            return accounts;
        }

        public static async Task<Transfer> EnableIntTransfer(TransferViewModel info)
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
            var response = await client.PostAsync("http://localhost:55303/transfertointacc", httpContent);

            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<Transfer>(jsonString);

            return accounts;
        }

        public static async Task<List<IntTransferStatesViewModel>> CheckIntTransferStates(int info)
        {
            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(info);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/states?CustID=" + info.ToString(), httpContent);

            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<List<IntTransferStatesViewModel>>(jsonString);

            return accounts;
        }

        public static async Task<Transfer> EnableBillPayment(TransferViewModel info)
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
            var response = await client.PostAsync("http://localhost:55303/billpaymentfromacc", httpContent);

            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<Transfer>(jsonString);

            return accounts;
        }

    }
}