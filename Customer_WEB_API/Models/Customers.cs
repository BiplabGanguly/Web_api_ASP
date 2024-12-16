using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer_WEB_API.Models
{
	public class Customers
	{
		public int CustomerId { get; set; }     
		public string FirstName { get; set; }   
		public string LastName { get; set; }     
		public string Phone { get; set; }        
		public string Email { get; set; }       
	}
}