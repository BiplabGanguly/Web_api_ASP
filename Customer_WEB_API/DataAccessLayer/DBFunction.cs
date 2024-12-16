using Customer_WEB_API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace Customer_WEB_API.DataAccessLayer
{
	public class DBFunction
	{
		string conStr = string.Empty;
		DataTable _dt;
		DataSet _ds;
		public DBFunction()
		{
			conStr = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
			_dt = new DataTable();
		}

		public List<Customers> getAllCustomers(string spName)
		{
			List<Customers> customers = new List<Customers>();
			using (SqlConnection con = new SqlConnection(conStr))
			{
				SqlCommand cmd = new SqlCommand(spName,con);
				cmd.CommandType = CommandType.StoredProcedure;
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows) 
				{
					while (reader.Read()) 
					{
						Customers cust = new Customers();
						cust.CustomerId = Convert.ToInt32(reader["customer_id"]);
						cust.FirstName = reader["first_name"].ToString();
						cust.LastName = reader["last_name"].ToString();
						cust.Email = reader["email"].ToString();
						cust.Phone = reader["phone"].ToString();
						customers.Add(cust);
					}
				}
			}
			return customers;	
		}


		public string CustomerAction(string query, Customers customer) 
		{
			string result = string.Empty;
			using (SqlConnection con = new SqlConnection(conStr))
			{
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.CommandType = CommandType.StoredProcedure;
				if (customer.CustomerId != 0)
				{
					cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
				}
				cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
				cmd.Parameters.AddWithValue("@LastName", customer.LastName);
				cmd.Parameters.AddWithValue("@Phone", customer.Phone);
				cmd.Parameters.AddWithValue("@Email", customer.Email);

				con.Open();
				int row = cmd.ExecuteNonQuery();
				if (row > 0) {
					result = "Successfull";
				}
				else
				{
					result = "Error Occured!";
				}
			}
			return result;
		
		}



		public string DeleteCustomer(int customerId) 
		{
			string msg = string.Empty;
			using (SqlConnection con = new SqlConnection(conStr))
			{
				SqlCommand cmd = new SqlCommand("[Test].[DeleteCustomer]", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@CustomerId", customerId);
				con.Open();
				int row = cmd.ExecuteNonQuery();
				if (row > 0)
				{
					msg = "Successfull";
				}
				else
				{
					msg = "Error Occured!";
				}
			}
			return msg;

		}






	}
}