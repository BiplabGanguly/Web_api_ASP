using Antlr.Runtime.Tree;
using Customer_WEB_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Customer_WEB_API.Controllers
{
	public class HomeController : Controller
	{
		HttpClient client = new HttpClient();
		string base_usl = $"https://localhost:44389/";
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";
			IEnumerable<Customers> cust = null;
			var consumeData = client.GetAsync($"{base_usl}Api/getdata");
			consumeData.Wait();
			var dataRead = consumeData.Result;
			if (dataRead.IsSuccessStatusCode)
			{
				var results = dataRead.Content.ReadAsAsync<IList<Customers>>();
				results.Wait();
				cust = results.Result;
			}
			else
			{
				cust = Enumerable.Empty<Customers>();
				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
			}
			return View(cust);
		}

		public ActionResult CreateCustomer()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> CreateCustomer(Customers customer)
		{


			var content = new StringContent(
				JsonConvert.SerializeObject(customer),
				Encoding.UTF8,
				"application/json"
			);

			HttpResponseMessage response = await client.PostAsync($"{base_usl}Api/insert", content);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Error occurred while creating customer.");
				return View(customer);
			}
		}

		public async Task<ActionResult> Delete(int id)
		{
			HttpClient client = new HttpClient();
			string base_usl = $"https://localhost:44389/";
			HttpResponseMessage response = await client.DeleteAsync($"{base_usl}Api/Delete/{id}");

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Error occurred while deleting the customer.");
				return RedirectToAction("Index");
			}
		}

		public ActionResult Edit(int id)
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Edit(int id,Customers customers)
		{

			var content = new StringContent(
				JsonConvert.SerializeObject(customers),
				Encoding.UTF8,
				"application/json"
			);
			HttpClient client = new HttpClient();
			string base_usl = $"https://localhost:44389/";
			HttpResponseMessage response = await client.PutAsync($"{base_usl}Api/Edit/{id}", content);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Error occurred while deleting the customer.");
				return RedirectToAction("Index");
			}
		}
	}
}
