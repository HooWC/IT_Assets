using IT_Assets_Insfrastructure.Repository.Api;
using IT_Assets_Insfrastructure.Repository.Token;
using Microsoft.AspNetCore.Mvc;

namespace IT_Assets.Controllers
{
	public class LoginController : Controller
	{
		public Admin_Api Ad = new Admin_Api();
		public AssetTypes_Api AT = new AssetTypes_Api();
		public EmployeeAssets_Api EA = new EmployeeAssets_Api();
		public Employees_Api Emp = new Employees_Api();
		public ITAssetInventory_Api IT_assetInven = new ITAssetInventory_Api();
		public ITAssets_Api IT_assets = new ITAssets_Api();

		public IActionResult AdminLogin()
		{
			HttpContext.Session.SetString("Admin", "");
			AdminToken.username = "";
			AdminToken.password = "";
			return View();
		}

		[HttpPost]
		public IActionResult AdminLogin_Post(string username, string password)
		{
			var all_ad = Ad.GetAllAdmin_Default().Result;
			var data = all_ad.Where(x => x.username == username && x.password == password).FirstOrDefault();

			if (data != null)
			{
				HttpContext.Session.SetString("Admin", data.id.ToString());
				AdminToken.username = username;
				AdminToken.password = password;
				return RedirectToAction("HomePage", "Admin");
			}

			return RedirectToAction("Home", "Home");
		}

		public IActionResult EmployeesLogin()
		{
			HttpContext.Session.SetString("User", "");
			EmployeeToken.username = "";
			EmployeeToken.password = "";
			return View();
		}

		[HttpPost]
		public IActionResult EmployeesLogin_Post(string username, string password)
		{
			var all_emp = Emp.GetAllEmployees_Admin_Default().Result;
			var data = all_emp.Where(x => x.username == username && x.password == password).FirstOrDefault();

			if (data != null)
			{
				HttpContext.Session.SetString("User", data.employee_id.ToString());
				EmployeeToken.username = username;
				EmployeeToken.password = password;
				return RedirectToAction("HomePage", "Employee");
			}

			return RedirectToAction("Home", "Home");
		}
	}
}
