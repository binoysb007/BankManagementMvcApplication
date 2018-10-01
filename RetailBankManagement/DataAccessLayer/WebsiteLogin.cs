using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using RetailBankManagement.Models;

namespace RetailBankManagement.DAL
{
    public class WebsiteLogin
    {
        public string Login(WebsiteLoginModels MD)
        {
            //string ConnectionString = "Data Source=mydbtest.csbpqeryrgzh.us-west-2.rds.amazonaws.com;" + "Initial Catalog=mydbtest;" + "User Id=mydbuser;" + "Password=Mydbuser12345678;";

            //string ConnectionString = "Data Source=tcp:bankmanagement.database.windows.net,1433;" + "Initial Catalog=RetailBankManagement;" + "User Id=Bank;" + "Password=Binoy@007;";
            string ConnectionString = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "WebsiteLoginProcedure_s2por";
            command.Parameters.AddWithValue("@username", MD.User_ID);
            command.Parameters.AddWithValue("@Password", MD.Password);
            command.Connection = connection;
            SqlDataReader Reader = command.ExecuteReader();
            if (Reader.Read())
            {
                string role = Reader["Role"].ToString();
                return role;
            }
            else
            {
                return "Invalid Username/Password";
            }
        }
    }
}