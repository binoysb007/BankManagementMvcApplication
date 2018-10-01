using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using RetailBankManagement.Models;
using RetailBankManagement.DataAccessLayer;


namespace RetailBankManagement.Controllers.CreateCustomerAccounts2porDataAccessLayer
{
    public class CreateCustomerAccounts2porDbData
    {
        /// <summary>
        /// Sql operation to crete a new account for an existing customer.
        /// </summary>
        /// <param name="meth"></param>
        /// <returns></returns>
        public int insertdata(CreateCustomerAccounts2por meth)
        {
            try
            {
                DateTime date = DateTime.Now;

                //  string connectionstring = "Data Source=intvmsql01;Initial Catalog =db02test01; User Id=pj02test01; password=tcstvm";
                string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Insert_Create_Customer_Account_s2por";
                command.Connection = connection;


                command.Parameters.AddWithValue("@CustomerID", meth.CustomerID);
                command.Parameters.AddWithValue("@AccountType", meth.AccountType);
                command.Parameters.AddWithValue("@DepositeAmount", meth.DepositAmount);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@AccountID", 0);
                command.Parameters.AddWithValue("@count", 0);
                command.Parameters.AddWithValue("@temp_type","");

                command.Parameters["@AccountID"].Direction = ParameterDirection.Output;
                int pp = command.ExecuteNonQuery();
                connection.Close();
                if (pp > 0)
                {
                    int x = Convert.ToInt32(command.Parameters["@AccountID"].Value);
                    return x;
                }

                else
                {
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// Sql operation for searching a particular account by its customer ID or account ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public DataSet AccountSearch(int id, string mode)
        {
            
            string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_AccountSearch_s2por";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@A_ID", id);
                cmd.Parameters.AddWithValue("@A_Mode", mode);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                connection.Close();
                return ds;
        }

        /// <summary>
        /// Sql operation to delete a particular account only if balance is zero.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="balance"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>

        public int DeleteAccount(int CustomerID , int balance , int AccountID)
        {
            try
            {
                DateTime date = DateTime.Now;
                //   string connectionstring = "server=intvmsql01; database=db02test01; user id=pj02test01; password=tcstvm";
                string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_DeleteAccount_s2por";
                command.Connection = connection;

                command.Parameters.AddWithValue("@CustomerID", CustomerID);
                command.Parameters.AddWithValue("@DepositeAmount", balance);
                command.Parameters.AddWithValue("@AccountID", AccountID);
                command.Parameters.AddWithValue("@date", date);
                int cc = command.ExecuteNonQuery();
                connection.Close();

                if (cc > 0)
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
        /// Sql operation to withdraw certain amount from the existing source account.
        /// </summary>
        /// <param name="ab"></param>
        /// <param name="latest"></param>
        /// <returns></returns>
        public int withdrawamount(AccountSearch ab, int latest)
        {
            try
            {
                DateTime date = DateTime.Now;
                
                string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_withdraw_s2por";
                command.Connection = connection;


                command.Parameters.AddWithValue("@accountid", ab.AccountID);
                command.Parameters.AddWithValue("@amount", latest);
                command.Parameters.AddWithValue("@value", ab.withdrawamount);
                command.Parameters.AddWithValue("@date", date);

                int rowaffected = command.ExecuteNonQuery();
                connection.Close();

                return rowaffected;
            }
            catch(Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// Sql operation to deposit certain amount to existing source account.
        /// </summary>
        /// <param name="ab"></param>
        /// <param name="latest"></param>
        /// <returns></returns>
        public int depositamount(AccountSearch ab, int latest)
        {
            try
            {
                DateTime date = DateTime.Now;

                
                string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_depositcash_s2por";
                command.Connection = connection;


                command.Parameters.AddWithValue("@accountid", ab.AccountID);
                command.Parameters.AddWithValue("@amount", latest);
                command.Parameters.AddWithValue("@value", ab.DepositAmount);
                command.Parameters.AddWithValue("@date", date);

                int rowaffected = command.ExecuteNonQuery();
                connection.Close();

                return rowaffected;
            }
            catch(Exception e)
            {
                return 0;
            }

        }
        /// <summary>
        /// Sql operation to view account statement based on number of transactions.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cyc"></param>
        /// <returns></returns>
        public List<AccountSearch> viewAccountStatement(int id,int cyc)
        {

            
            string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_viewAccountstatement_s2por";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@acc_id", id);
                cmd.Parameters.AddWithValue("@no", cyc);

                List<AccountSearch> aclist = new List<AccountSearch>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int t_id = Convert.ToInt32(reader["TransactionID"]);
                    string descript = reader["Dessript"].ToString();
                    DateTime Date = Convert.ToDateTime(reader["Trnct_date"]);
                    int amount = Convert.ToInt32(reader["Amount"]);
                    AccountSearch ac = new AccountSearch(t_id, descript, Date, amount);
                    aclist.Add(ac);

                }
                connection.Close();
                return (aclist);
            
            
        }

        /// <summary>
        /// Sql operation to view balance for a particular account ID.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int viewbalance(int r)
        {
            try
            {
                int b = 0;
                
                string str = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
                SqlConnection con = new SqlConnection(str);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "viewbalance_s2por";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@id", r);

                cmd.Parameters.AddWithValue("@bal", 20);

                cmd.Parameters["@bal"].Direction = ParameterDirection.Output;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    b = Convert.ToInt32(reader["DepositAmount"]);

                }
                return b;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        /// <summary>
        /// Sql operation to transfer certain amount from existing source account to target account.
        /// </summary>
        /// <param name="balancelist"></param>
        /// <param name="ta"></param>
        /// <returns></returns>
        public int transfer_amount(List<int> balancelist,transfer_amount ta)
        {
            try
            {
                DateTime date = DateTime.Now;
                int firstaccountupdate = balancelist.ElementAt(2);
                int secondaccountupdate = balancelist.ElementAt(3);
                
                string str = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
                SqlConnection con = new SqlConnection(str);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_updatebalance_s2por";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@amount1", firstaccountupdate);
                cmd.Parameters.AddWithValue("@amount2", secondaccountupdate);
                cmd.Parameters.AddWithValue("@id1", ta.source_account_id);
                cmd.Parameters.AddWithValue("@id2", ta.target_account_id);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@balance", ta.Transfer_amount);
                cmd.Parameters.AddWithValue("@count", 0);
                int rowaffect = cmd.ExecuteNonQuery();
                con.Close();
                if (rowaffect > 0)
                {
                    return 1;
                }
                else
                    return 2;
  
            }
            catch(Exception p)
            {
                return 0;
            }
        }

        /// <summary>
        /// Sql operation to view all the account details.
        /// </summary>
        /// <returns></returns>
        public List<AccountSearch> viewALLAccount()
        {
            
            string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "View_Account_StatusAll_s2por_Procedure";
                cmd.Connection = connection;
                List<AccountSearch> aclist = new List<AccountSearch>();
                SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int a_id = Convert.ToInt32(reader["AccountID"]);
                        int c_id = Convert.ToInt32(reader["CustomerID"]);
                        string acc_type = reader["AccountType"].ToString();
                        string status = reader["Status"].ToString();
                        string message = reader["Message"].ToString();
                        DateTime up_date = Convert.ToDateTime(reader["Last_Updated"]);
                        AccountSearch cus = new AccountSearch(a_id, c_id, acc_type, status, message, up_date);
                        aclist.Add(cus);
                    }
                connection.Close();
                return (aclist);
            }

        /// <summary>
        /// To check account is present in the database or not.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int checkAccount(int id, string name)
        {
            
            string connectionstring = "data source=.;" + "database=Binoy;" + "Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_checkAccount_s2por";
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@count", 0);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters["@count"].Direction = ParameterDirection.Output;
            int check = cmd.ExecuteNonQuery();
            connection.Close();
            return Convert.ToInt32(cmd.Parameters["@count"].Value); ;
        }
        }
    }