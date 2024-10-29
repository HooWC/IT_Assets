using System.Globalization;
using IT_Assets_Insfrastructure.Repository.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Assets.Controllers
{
	public class EmployeeController : Controller
	{
		public Admin_Api Ad = new Admin_Api();
		public AssetTypes_Api AT = new AssetTypes_Api();
		public EmployeeAssets_Api EA = new EmployeeAssets_Api();
		public Employees_Api Emp = new Employees_Api();
		public ITAssetInventory_Api IT_assetInven = new ITAssetInventory_Api();
		public ITAssets_Api IT_assets = new ITAssets_Api();

		public IActionResult HomePage()
		{
			string userValue = HttpContext.Session.GetString("User");
			var all_emp = Emp.GetAllEmployees().Result;
			var data = all_emp.Where(x => x.employee_id == Convert.ToInt32(userValue)).FirstOrDefault();
			ViewBag.Name = data.last_name + " " + data.first_name;
			ViewBag.avatar = data.avatar;
			ViewBag.department = data.department;
			ViewBag.position = data.position;
			ViewBag.email = data.email_address;
			ViewBag.phone = data.extension;
			ViewBag.username = data.username;
			ViewBag.password = data.password;
			ViewBag.first = data.first_name;
			ViewBag.last = data.last_name;

			var all_emp_assets = EA.GetAllEmployeeAssets().Result;
			var get_use = all_emp_assets.Where(x => x.status == false && x.leased.Contains("true") && x.employee_id == data.employee_id).Count();
			var get_application = all_emp_assets.Where(x => x.leased.Contains("false") && x.employee_id == data.employee_id).Count();

			ViewBag.use = get_use;
			ViewBag.application = get_application;

			string formattedDate = DateTime.Now.ToString("MMMM, d", new CultureInfo("en-US"));

			ViewBag.Date = formattedDate;

			var it_in = IT_assetInven.GetAllITAssetInventory().Result;
			var it_asset = IT_assets.GetAllITAssets().Result;
			var it_data = it_asset.Where(x => x.status == true).ToList();

			var db = (from c in it_asset
					  where c.status == true
					  join emp in it_in on c.asset_id equals emp.asset_id
					  where emp.number_in_stock > 0
					  select new
					  {
						  asset_id = c.asset_id,
						  description = c.description
					  }).ToList();

			ViewBag.list = new SelectList(db, "asset_id", "description");

			return View();
		}
	}
}
