﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance 
        { 
            get { if (instance == null) instance = new AccountDAO(); return instance; } 
            private set => instance = value; 
        }

        private AccountDAO() { }

        public bool Login(string username, string pass)
        {
            string query = "USP_Login @username , @pass";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {username, pass});
            return result.Rows.Count > 0; //Trả ra kết quả có số dòng > 0
        }
    }
}
