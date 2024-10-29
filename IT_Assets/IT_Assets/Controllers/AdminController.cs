using System.Collections.Generic;
using IT_Assets_DomainModelEntity.Models;
using IT_Assets_Insfrastructure.Repository.Api;
using Microsoft.AspNetCore.Mvc;

namespace IT_Assets.Controllers
{
	public class AdminController : Controller
	{
		public Admin_Api Ad = new Admin_Api();
		public AssetTypes_Api AT = new AssetTypes_Api();
		public EmployeeAssets_Api EA = new EmployeeAssets_Api();
		public Employees_Api Emp = new Employees_Api();
		public ITAssetInventory_Api IT_assetInven = new ITAssetInventory_Api();
		public ITAssets_Api IT_assets = new ITAssets_Api();


		public IActionResult HomePage()
		{
			string adminValue = HttpContext.Session.GetString("Admin");
			var all_admin = Ad.GetAllAdmin().Result;
			var db = all_admin.Where(x => x.id == Convert.ToInt32(adminValue)).FirstOrDefault();

			var IT_asset = IT_assets.GetAllITAssets_Admin().Result;
			var Emp_data = Emp.GetAllEmployees_Admin().Result;
			var Emp_data_asset = EA.GetAllEmployeeAssets_Admin().Result;
			var Emp_data_asset_count = Emp_data_asset.Select(e => e.employee_id).Distinct().Count();
			var asset_inventory_data_asset = IT_assetInven.GetAllITAssetInventory_Admin().Result;


			int totalAssigned = asset_inventory_data_asset.Sum(e => e.number_assigned);
			int totalStock = asset_inventory_data_asset.Sum(e => e.number_in_stock);

			float deviceUsageRate = ((float)totalAssigned / totalStock) * 100;



			// 计算每个部门的员工数
			var departmentCounts = Emp_data.GroupBy(e => e.department)
									  .ToDictionary(g => g.Key, g => g.Count());

			// 计算总员工数
			int totalEmployees = Emp_data.Count;

			// 计算每个部门的占比并限制在 0% 到 100% 之间
			Dictionary<string, double> departmentRatios = new Dictionary<string, double>();
			foreach (var department in departmentCounts.Keys)
			{
				double ratio = ((double)departmentCounts[department] / totalEmployees) * 100;
				ratio = Math.Min(100, Math.Max(0, ratio)); // 限制在 0% 到 100% 之间
				departmentRatios.Add(department, ratio);
			}

			ViewBag.HR = departmentRatios.ContainsKey("Human Resources") ? departmentRatios["Human Resources"] : 0;
			ViewBag.Tec = departmentRatios.ContainsKey("Technology") ? departmentRatios["Technology"] : 0;
			ViewBag.Sales = departmentRatios.ContainsKey("Sales") ? departmentRatios["Sales"] : 0;
			ViewBag.Marketing = departmentRatios.ContainsKey("Marketing") ? departmentRatios["Marketing"] : 0;
			ViewBag.Finance = departmentRatios.ContainsKey("Finance") ? departmentRatios["Finance"] : 0;
			ViewBag.CS = departmentRatios.ContainsKey("Customer Service") ? departmentRatios["Customer Service"] : 0;
			ViewBag.SP = departmentRatios.ContainsKey("Strategic Planning") ? departmentRatios["Strategic Planning"] : 0;
			ViewBag.IA = departmentRatios.ContainsKey("Internal Audit") ? departmentRatios["Internal Audit"] : 0;

			ViewBag.name = db.fullname;
			ViewBag.asset_count = IT_asset.Count;
			ViewBag.asset_count_data = (((float)IT_asset.Count() / 100) * 100).ToString("0");
			ViewBag.emp_count = Emp_data_asset_count;
			ViewBag.emp = (((float)Emp_data_asset_count / Emp_data.Count()) * 100).ToString("0");
			ViewBag.assigned_count = totalAssigned;
			ViewBag.assigned = (((float)totalAssigned / totalStock) * 100).ToString("0");

			ViewBag.count = Application_Count();
			return View();
		}

		public IActionResult Employees_List()
		{
			ViewBag.count = Application_Count();
			return View();
		}

		public IActionResult Assets_List()
		{
			ViewBag.count = Application_Count();
			return View();
		}

		public IActionResult Assets_Add()
		{
			ViewBag.count = Application_Count();
			return View();
		}

		public IActionResult Assets_Detail(int asset_id)
		{
			ViewBag.count = Application_Count();
			ViewBag.id = asset_id;
			return View();
		}

		public IActionResult Application()
		{
			ViewBag.count = Application_Count();

			return View();
		}

		public int Application_Count()
		{
			var all_ea = EA.GetAllEmployeeAssets_Admin().Result;
			var all_it = IT_assets.GetAllITAssets_Admin().Result;
			var all_emp = Emp.GetAllEmployees_Admin().Result;

			int no = 1;
			var db = (from c in all_ea
					  where c.leased.Contains("false")
					  join emp in all_emp on c.employee_id equals emp.employee_id
					  join it in all_it on c.asset_id equals it.asset_id
					  select new
					  {
						  no = no++,
					  }).ToList();

			return db.Count();
		}

	}
}
