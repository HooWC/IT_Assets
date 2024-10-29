using Microsoft.AspNetCore.Mvc;

namespace IT_Assets.Controllers
{
	public class LoadingController : Controller
	{
		public IActionResult AdminLogin()
		{
			return View();
		}

		public IActionResult EmployeeLogin()
		{
			return View();
		}
	}
}
