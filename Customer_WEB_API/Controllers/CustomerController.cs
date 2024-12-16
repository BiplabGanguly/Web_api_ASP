using Customer_WEB_API.BusinessLogic;
using Customer_WEB_API.DataAccessLayer;
using Customer_WEB_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Customer_WEB_API.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpGet]
        [Route("Api/getdata")]
        public IHttpActionResult GetCustomers()
        {
			try
			{
				CustomerService customerService = new CustomerService();
				List<Customers> cust = customerService.GetAllCustomers();
				return Ok(cust);
			}
			catch (ArgumentNullException argEx)
			{
				return BadRequest($"Invalid input: {argEx.Message}");
			}
		}

        [HttpPost]
		[Route("Api/insert")]
		public IHttpActionResult InsertCustomers([FromBody] Customers customers)
        {
            CustomerService customerService = new CustomerService();
            string msg = customerService.InsertCustomer(customers);
            return Ok(msg);
        }

        [HttpPut]
		[Route("Api/Edit/{id}")]
		public IHttpActionResult UpdateCustomers(int id,[FromBody] Customers customers)
        {
            customers.CustomerId = id;
			CustomerService customerService = new CustomerService();
			string msg = customerService.Update(customers);
			return Ok(msg);
		}

        [HttpDelete]
		[Route("Api/Delete/{id}")]
		public IHttpActionResult DeleteCustomers(int id) 
        {
            DBFunction db = new DBFunction();
            string msg = db.DeleteCustomer(id);
            return Ok(msg);
		}
    }
}
