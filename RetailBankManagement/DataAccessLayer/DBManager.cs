using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using RetailBankManagement.Models;
using System.Configuration;

namespace RetailBankManagement.DataAccessLayer
{
    public class DBManager
    {
        /// <summary>
        /// Sql operation for creating customer by unique SSN Id.
        /// If we enter same SSN Id then it will throw an sql exception which is catched and displayed as pop up message.
        /// </summary>
        /// <param name="cu"></param>
        /// <returns></returns>
        public int addCustomer(Customer cu)
        {
            try
            {
                DateTime date = DateTime.Now;
                
                string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_addCustomer_s2por";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Customer_SSN_ID", cu.Customer_ssn_Id);
                cmd.Parameters.AddWithValue("@Customer_Name", cu.Customer_Name);
                cmd.Parameters.AddWithValue("@Customer_Age", cu.Customer_Age);
                cmd.Parameters.AddWithValue("@Customer_Address", cu.Customer_Address);
                cmd.Parameters.AddWithValue("@Customer_State", cu.Customer_State);
                cmd.Parameters.AddWithValue("@Customer_City", cu.Customer_City);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@Customer_ID", 0);
                cmd.Parameters["@Customer_ID"].Direction = ParameterDirection.Output;

                int Rowaffected = cmd.ExecuteNonQuery();
                connection.Close();
                if (Rowaffected > 0)
                {
                    return Convert.ToInt32(cmd.Parameters["@Customer_ID"].Value);
                }
                else
                {
                    return 0;
                }
            }
                catch(SqlException e)
                    {
                        return 1;
                    }
            
        }
        /// <summary>
        /// Sql operation for searching a particular customer by its customer ID or customer SSNID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public DataSet viewCustomerbyID(int id,string mode)
        {
            
            string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_viewCustomerbyID_s2por";
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@c_id", id);
            cmd.Parameters.AddWithValue("@c_mode", mode);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            return ds;
            
        }
        /// <summary>
        ///  Sql operation for deleting a particular customer if no account exist corresponding to it.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int deleteCustomer(Customer obj)
        {
            try
            {
                
                string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
                SqlConnection connection = new SqlConnection(connectionstring);

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_deletecustomer14_s2por";
                command.Connection = connection;
                command.Parameters.AddWithValue("@customerid", obj.Customer_Id);
                command.Parameters.AddWithValue("@count", 0);
                int rowAffected = command.ExecuteNonQuery();
                connection.Close();
                if (rowAffected > 0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        
        }
        /// <summary>
        ///  Sql operation for updating a particular customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public string UpdateCustomer(Customer customer)
        {
            DateTime date = DateTime.Now;
           
            string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "updatecustomer_s2por";
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@Customer_ID", customer.Customer_Id);
            cmd.Parameters.AddWithValue("@Customer_SSN_ID", customer.Customer_ssn_Id);
            cmd.Parameters.AddWithValue("@Customer_Name", customer.Customer_Name);
            cmd.Parameters.AddWithValue("@Customer_Age", customer.Customer_Age);
            cmd.Parameters.AddWithValue("@Customer_Address", customer.Customer_Address);
            cmd.Parameters.AddWithValue("@Customer_State", customer.Customer_State);
            cmd.Parameters.AddWithValue("@Customer_City", customer.Customer_City);
            cmd.Parameters.AddWithValue("@Update_date", date);
            string result = cmd.ExecuteNonQuery().ToString();
            connection.Close();
            return result;
        }
       /// <summary>
        /// Sql operation for displaying all customer details.
       /// </summary>
       /// <returns></returns>
        public List<Customer> ViewAllCustomer()
        {
            
            string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "View_Customer_StatusAll_s2por_procedure";
            cmd.Connection = connection;
            List<Customer> clist = new List<Customer>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int c_id = Convert.ToInt32(reader["Customer_ID"]);
                int ssn_id = Convert.ToInt32(reader["Customer_SSNID"]);
                string status = reader["Status"].ToString();
                string message = reader["Message"].ToString();
                DateTime up_date = Convert.ToDateTime(reader["Last_Updated"]);
                Customer cus = new Customer(c_id, ssn_id, status, message, up_date);
                clist.Add(cus);
            }
            connection.Close();
            return (clist);
        }
        /// <summary>
        /// To check customer is present in the database or not.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int checkCustomer(int id,string name)
        {
            
            string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_checkCustomer_s2por";
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@count", 0);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters["@count"].Direction = ParameterDirection.Output;
            int check = cmd.ExecuteNonQuery();
            connection.Close();
            return Convert.ToInt32(cmd.Parameters["@count"].Value);
        }


    }
}