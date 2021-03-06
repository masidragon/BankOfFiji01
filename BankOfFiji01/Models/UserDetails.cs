﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class UserDetails
    {

        public UserDetails()
        {
            AccountList = new List<Account>();
            InterestEarned=new List<TransactionHistory>();
            MonthlyFees=new List<TransactionHistory>();
        }
        public string FirstName { get; set; }
        public int RoleID { get; set; }
        public int CustomerID { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string FNPFNumber { get; set; }
        public string ResidentialStatus { get; set; }
        public string HomeAddress { get; set; }
        public string PostalAddress { get; set; }
        public List<Account> AccountList { get; set; }
        public List<TransactionHistory> InterestEarned { get; set; }
        public List<TransactionHistory> MonthlyFees { get; set; }
    }
}