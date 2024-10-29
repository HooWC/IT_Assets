using System.Diagnostics;
using IT_Assets.Models;
using IT_Assets_Insfrastructure.Repository.Token;
using Microsoft.AspNetCore.Mvc;

namespace IT_Assets.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Home()
		{
			HttpContext.Session.SetString("User", "");
			EmployeeToken.username = "";
			EmployeeToken.password = "";
			HttpContext.Session.SetString("Admin", "");
			AdminToken.username = "";
			AdminToken.password = "";
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}
	}
}