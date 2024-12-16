using Customer_WEB_API.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Customer_WEB_API.Models;

namespace Customer_WEB_API.BusinessLogic
{
	public class CustomerService
	{
		DBFunction _df;
		DataTable _dt;
		SqlParameter[] _param;
		public CustomerService()
		{
			_df = new DBFunction();
		}

		public List<Customers> GetAllCustomers()
		{
			return _df.getAllCustomers("Test.USPGetAllCustomer");
		}

		public string InsertCustomer(Customers customers)
		{
			return _df.CustomerAction("[Test].[InsertCustomer]",customers);
		}


		public string Update(Customers customers)
		{
			return _df.CustomerAction("[Test].[UpdateMultipleCustomers]", customers);
		}

	}
}