using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using IT_Assets_DomainModelEntity.Models;
using IT_Assets_Insfrastructure.Repository.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;

namespace IT_Assets.Controllers
{
	public class AjaxController : Controller
	{
		public Admin_Api Ad = new Admin_Api();
		public AssetTypes_Api AT = new AssetTypes_Api();
		public EmployeeAssets_Api EA = new EmployeeAssets_Api();
		public Employees_Api Emp = new Employees_Api();
		public ITAssetInventory_Api IT_assetInven = new ITAssetInventory_Api();
		public ITAssets_Api IT_assets = new ITAssets_Api();
		public Contact_Api Con = new Contact_Api();

		public IActionResult Get_Employees_List()
		{
			var employee_list = Emp.GetAllEmployees_Admin_Default().Result;
			var all_employee_asset = EA.GetAllEmployeeAssets_Admin_Default().Result;

			List<int> list = new List<int>();
			int num = 0;
			foreach (var i in employee_list)
			{
				num = 0;
				foreach (var e in all_employee_asset)
				{
					if (i.employee_id == e.employee_id && e.status == false && e.leased.Contains("true"))
					{
						num += 1;
					}
				}
				list.Add(num);
			}

			int increment = 0;
			var db = (from c in employee_list
					  select new
					  {
						  employee_id = c.employee_id,
						  first_name = c.first_name,
						  last_name = c.last_name,
						  department = c.department,
						  avatar = c.avatar,
						  position = c.position,
						  number = list[increment++]
					  }).ToList();

			return Json(db);
		}

		public IActionResult Get_Employees_First()
		{
			var employee_list = Emp.GetAllEmployees_Admin_Default().Result;

			return Json(employee_list.FirstOrDefault());
		}

		public IActionResult Get_Employees_Asset_List_First()
		{
			var employee_list = Emp.GetAllEmployees_Admin_Default().Result;
			var id = employee_list.FirstOrDefault().employee_id;

			var all_employee_asset = EA.GetAllEmployeeAssets_Admin_Default().Result;
			var IT_Asset = IT_assets.GetAllITAssets_Admin_Default().Result;


			var db = (from c in all_employee_asset
					  where c.employee_id == id && c.status == false && c.leased.Contains("true")
					  join it in IT_Asset on c.asset_id equals it.asset_id
					  select new
					  {
						  date_out = c.date_out.ToString("yyyy-MM-dd"),
						  condition_out = c.condition_out,
						  other_details = c.other_details,
						  asset_type_code = it.asset_type_code,
						  assetImage = it.assetImage,
						  description = it.description,
						  name = employee_list.FirstOrDefault().last_name + " " + employee_list.FirstOrDefault().first_name,
						  avatar = employee_list.FirstOrDefault().avatar,
						  department = employee_list.FirstOrDefault().department,
						  position = employee_list.FirstOrDefault().position,
						  employee_asset = c.id,
					  }).ToList();


			return Json(db);
		}

		[HttpPost]
		public IActionResult Get_Employees_Asset_List_Other(int employee_id)
		{
			var employee_list = Emp.GetAllEmployees_Admin_Default().Result;
			var one = employee_list.Where(x => x.employee_id == employee_id).FirstOrDefault();
			//var id = employee_list.FirstOrDefault().employee_id;

			var all_employee_asset = EA.GetAllEmployeeAssets_Admin_Default().Result;
			var IT_Asset = IT_assets.GetAllITAssets_Admin_Default().Result;

			Boolean b = false;
			foreach (var i in all_employee_asset)
			{
				if (i.employee_id == employee_id && i.leased.Contains("true") && i.status == false)
					b = true;
			}

			if (b == true)
			{
				var db = (from c in all_employee_asset
						  where c.employee_id == one.employee_id && c.status == false && c.leased.Contains("true")
						  join it in IT_Asset on c.asset_id equals it.asset_id
						  select new
						  {
							  date_out = c.date_out.ToString("yyyy-MM-dd"),
							  date_return = c.date_returned.ToString("yyyy-MM-dd"),
							  condition_out = c.condition_out,
							  other_details = c.other_details,
							  asset_type_code = it.asset_type_code,
							  assetImage = it.assetImage,
							  description = it.description,
							  name = one.last_name + " " + one.first_name,
							  avatar = one.avatar,
							  b = true,
							  department = one.department,
							  position = one.position,
							  employee_id = one.employee_id,
							  first_name = one.first_name,
							  last_name = one.last_name,
							  email_address = one.email_address,
							  username = one.username,
							  password = one.password,
							  extension = one.extension,
							  employee_asset = c.id,
						  }).ToList();


				return Json(db);
			}

			var other_db = (from c in employee_list
							where c.employee_id == employee_id
							select new
							{
								name = c.last_name + " " + c.first_name,
								avatar = c.avatar,
								b = false,
								department = one.department,
								position = one.position,
								employee_id = one.employee_id,
								first_name = one.first_name,
								last_name = one.last_name,
								email_address = one.email_address,
								username = one.username,
								password = one.password,
								extension = one.extension,
								employee_asset = 0,
							}).ToList();


			return Json(other_db);

		}

		[HttpPost]
		public IActionResult Search_Employees(string Search)
		{
			var employee_list = Emp.GetAllEmployees_Admin_Default().Result;
			var all_employee_asset = EA.GetAllEmployeeAssets_Admin_Default().Result;

			if (Search != "" && Search != null)
			{
				var one = employee_list.Where(x => (x.last_name + " " + x.first_name).Contains(Search)).ToList();

				List<KeyValuePair<string, int>> list_inter = new List<KeyValuePair<string, int>>();
				int num_inter = 0;
				foreach (var i in one)
				{
					num_inter = 0;
					foreach (var e in all_employee_asset)
					{
						if (i.employee_id == e.employee_id && e.status == false && e.leased.Contains("true"))
						{
							num_inter += 1;
						}
					}
					list_inter.Add(new KeyValuePair<string, int>(i.last_name + " " + i.first_name, num_inter));
				}

				var db_inter = (from c in one
								select new
								{
									employee_id = c.employee_id,
									first_name = c.first_name,
									last_name = c.last_name,
									department = c.department,
									avatar = c.avatar,
									position = c.position,
									number = list_inter.FirstOrDefault(kv => kv.Key == c.last_name + " " + c.first_name).Value
								}).ToList();

				return Json(db_inter);
			}



			List<int> list = new List<int>();
			int num = 0;
			foreach (var i in employee_list)
			{
				num = 0;
				foreach (var e in all_employee_asset)
				{
					if (i.employee_id == e.employee_id && e.status == false && e.leased.Contains("true"))
					{
						num += 1;
					}
				}
				list.Add(num);
			}

			int increment = 0;
			var db = (from c in employee_list
					  select new
					  {
						  employee_id = c.employee_id,
						  first_name = c.first_name,
						  last_name = c.last_name,
						  department = c.department,
						  avatar = c.avatar,
						  position = c.position,
						  number = list[increment++]
					  }).ToList();

			return Json(db);

		}

		public IActionResult Get_Assets_List()
		{
			var it_assets_list = IT_assets.GetAllITAssets_Admin_Default().Result;
			var it_asset_inventory = IT_assetInven.GetAllITAssetInventory_Admin_Default().Result;


			var db = (from c in it_assets_list
					  join inventory in it_asset_inventory on c.asset_id equals inventory.asset_id
					  select new
					  {
						  asset_id = c.asset_id,
						  asset_type_code = c.asset_type_code,
						  assetImage = c.assetImage,
						  description = c.description,
						  status = c.status,
						  other_details = c.other_details,
						  number_in_stock = inventory.number_in_stock,
						  number_assigned = inventory.number_assigned,
					  }).ToList();

			return Json(db);
		}

		[HttpPost]
		public IActionResult Search_Assets(string Search)
		{
			var it_assets_list = IT_assets.GetAllITAssets_Admin_Default().Result;
			var it_asset_inventory = IT_assetInven.GetAllITAssetInventory_Admin_Default().Result;

			if (Search != "" && Search != null)
			{
				var one = it_assets_list.Where(x => x.description.Contains(Search)).ToList();

				var db_inter = (from c in one
								join inventory in it_asset_inventory on c.asset_id equals inventory.asset_id
								select new
								{
									asset_id = c.asset_id,
									asset_type_code = c.asset_type_code,
									assetImage = c.assetImage,
									description = c.description,
									status = c.status,
									other_details = c.other_details,
									number_in_stock = inventory.number_in_stock,
									number_assigned = inventory.number_assigned,
								}).ToList();

				return Json(db_inter);
			}

			var db = (from c in it_assets_list
					  join inventory in it_asset_inventory on c.asset_id equals inventory.asset_id
					  select new
					  {
						  asset_id = c.asset_id,
						  asset_type_code = c.asset_type_code,
						  assetImage = c.assetImage,
						  description = c.description,
						  status = c.status,
						  other_details = c.other_details,
						  number_in_stock = inventory.number_in_stock,
						  number_assigned = inventory.number_assigned,
					  }).ToList();

			return Json(db);

		}

		[HttpPost]
		public IActionResult Assets_Detail(int asset_id)
		{
			var it_assets_list = IT_assets.GetAllITAssets_Admin_Default().Result;
			var it_asset_inventory = IT_assetInven.GetAllITAssetInventory_Admin_Default().Result;
			var assets_type = AT.GetAllAssetTypes_Admin_Default().Result;

			var db = (from c in it_assets_list
					  where c.asset_id == asset_id
					  join inventory in it_asset_inventory on c.asset_id equals inventory.asset_id
					  join assetstype in assets_type on c.asset_type_code equals assetstype.asset_type_code
					  select new
					  {
						  asset_id = c.asset_id,
						  asset_type_code = c.asset_type_code,
						  assetImage = c.assetImage,
						  description = c.description,
						  status = c.status == true ? "True" : "False",
						  other_details = c.other_details,
						  number_in_stock = inventory.number_in_stock,
						  number_assigned = inventory.number_assigned,
						  inventory_other_details = inventory.other_details,
						  assettype_detail = assetstype.asset_type_description,
						  other_in_detail = inventory.other_details
					  }).FirstOrDefault();

			return Json(db);

		}

		public IActionResult Get_Employees_Asset_List()
		{
			string userValue = HttpContext.Session.GetString("User");
			var all_emp = Emp.GetAllEmployees().Result;
			var all_ad = Ad.GetAllAdmin_Default().Result;
			var data = all_emp.Where(x => x.employee_id == Convert.ToInt32(userValue)).FirstOrDefault();

			var all_employee_asset = EA.GetAllEmployeeAssets_Admin_Default().Result;
			var It_asset = IT_assets.GetAllITAssets().Result;
			var all_asset = all_employee_asset.Where(x => x.employee_id == data.employee_id && x.status == false).ToList();

			string formattedDate = DateTime.Now.ToString("yyyy-MM-dd");
			List<KeyValuePair<int, int>> list_inter = new List<KeyValuePair<int, int>>();
			List<KeyValuePair<int, double>> list_inter2 = new List<KeyValuePair<int, double>>();
			foreach (var i in all_asset)
			{
				string formattedDate1 = formattedDate;
				string formattedDate2 = i.date_returned.ToString("yyyy-MM-dd");

				DateTime date1 = DateTime.Parse(formattedDate1);
				DateTime date2 = DateTime.Parse(formattedDate2);

				int compareResult = DateTime.Compare(date1, date2);
				int daysRemaining = 0;

				if (compareResult < 0)
				{
					daysRemaining = (date2 - date1).Days;
				}
				else if (compareResult > 0)
				{
					daysRemaining = (date1 - date2).Days;
				}

				DateTime startDate = DateTime.Parse(i.date_out.ToString());
				DateTime endDate = DateTime.Parse(i.date_returned.ToString("yyyy-MM-dd"));
				DateTime currentDate = DateTime.Now;

				double totalDays = (endDate - startDate).TotalDays;
				double elapsedDays = (currentDate - startDate).TotalDays;

				double progressPercentage = Math.Max(0, Math.Min(100, (elapsedDays / totalDays) * 100));
				double roundedNumber = Math.Round(progressPercentage, 0);

				list_inter.Add(new KeyValuePair<int, int>(i.id, daysRemaining));
				list_inter2.Add(new KeyValuePair<int, double>(i.id, roundedNumber));
			}

			var db = (from c in all_asset
					  where c.leased.Contains("true")
					  join asset in It_asset on c.asset_id equals asset.asset_id
					  join ad in all_ad on c.admin equals ad.id
					  select new
					  {
						  id = c.id,
						  date_out = c.date_out.ToString("yyyy-MM-dd"),
						  date_return = c.date_returned.ToString("yyyy-MM-dd"),
						  image = asset.assetImage,
						  description = asset.description,
						  other_details = c.other_details,
						  left_day = list_inter.FirstOrDefault(kv => kv.Key == c.id).Value,
						  count = list_inter2.FirstOrDefault(kv => kv.Key == c.id).Value,
						  avatar = ad.avatar
					  }).ToList();

			return Json(db);
		}

		public IActionResult Get_Employees_History_List()
		{
			string userValue = HttpContext.Session.GetString("User");
			var all_emp = Emp.GetAllEmployees().Result;
			var data = all_emp.Where(x => x.employee_id == Convert.ToInt32(userValue)).FirstOrDefault();

			var all_employee_asset = EA.GetAllEmployeeAssets_Admin_Default().Result;
			var It_asset = IT_assets.GetAllITAssets().Result;
			var all_asset = all_employee_asset.Where(x => x.employee_id == data.employee_id && x.status == false).ToList();

			var db = (from c in all_employee_asset
					  where c.employee_id == data.employee_id && c.status == true && c.leased.Contains("true")
					  join asset in It_asset on c.asset_id equals asset.asset_id
					  select new
					  {
						  date_out = c.date_out.ToString("yyyy-MM-dd"),
						  date_return = c.date_returned.ToString("yyyy-MM-dd"),
						  image = asset.assetImage,
						  description = asset.description,
						  condition = c.condition_returned,
						  other_details = c.other_details
					  }).ToList();

			return Json(db);
		}

		public IActionResult Get_Employees_Documents()
		{

			var It_asset = IT_assets.GetAllITAssets().Result;
			var It_asset_Inventory = IT_assetInven.GetAllITAssetInventory().Result;

			int count = 1;
			var db = (from c in It_asset
					  join assetInventory in It_asset_Inventory on c.asset_id equals assetInventory.asset_id
					  select new
					  {
						  id = count++,
						  image = c.assetImage,
						  description = c.description,
						  type = c.asset_type_code,
						  status = c.status == true ? "Active" : "Deactivated",
						  stock = assetInventory.number_in_stock,
						  color = c.status == true ? "delivered" : "cancelled"
					  }).ToList();

			return Json(db);
		}

		[HttpPost]
		public IActionResult Search_Assets_List_Employee(string Search)
		{
			var it_assets_list = IT_assets.GetAllITAssets_Admin_Default().Result;
			var It_asset_Inventory = IT_assetInven.GetAllITAssetInventory_Admin_Default().Result;

			if (Search != "" && Search != null)
			{
				var one = it_assets_list.Where(x => x.description.Contains(Search)).ToList();

				int count_inter = 1;
				var db_inter = (from c in one
								join assetInventory in It_asset_Inventory on c.asset_id equals assetInventory.asset_id
								select new
								{
									id = count_inter++,
									image = c.assetImage,
									description = c.description,
									type = c.asset_type_code,
									status = c.status == true ? "Active" : "Deactivated",
									stock = assetInventory.number_in_stock,
									color = c.status == true ? "delivered" : "cancelled"
								}).ToList();

				return Json(db_inter);
			}

			int count = 1;
			var db = (from c in it_assets_list
					  join assetInventory in It_asset_Inventory on c.asset_id equals assetInventory.asset_id
					  select new
					  {
						  id = count++,
						  image = c.assetImage,
						  description = c.description,
						  type = c.asset_type_code,
						  status = c.status == true ? "Active" : "Deactivated",
						  stock = assetInventory.number_in_stock,
						  color = c.status == true ? "delivered" : "cancelled"
					  }).ToList();

			return Json(db);

		}

		public IActionResult Get_Employees_Edit_Form_First()
		{
			var employee_list = Emp.GetAllEmployees_Admin_Default().Result;
			var db = employee_list.ToList();


			return Json(db);
		}

		[HttpPost]
		public IActionResult Get_Employees_Asset_Detail_Model(int employeeassetID)
		{
			var all_employee_asset = EA.GetAllEmployeeAssets_Admin_Default().Result;
			var employee_list = Emp.GetAllEmployees_Admin_Default().Result;

			var IT_Asset = IT_assets.GetAllITAssets_Admin_Default().Result;


			var db = (from c in all_employee_asset
					  where c.id == employeeassetID
					  join emp in employee_list on c.employee_id equals emp.employee_id
					  join it in IT_Asset on c.asset_id equals it.asset_id
					  select new
					  {
						  date_out = c.date_out.ToString("yyyy-MM-dd"),
						  date_return = c.date_returned.ToString("yyyy-MM-dd"),
						  other_details = c.other_details,
						  asset_type_code = it.asset_type_code,
						  assetImage = it.assetImage,
						  description = it.description,
						  name = emp.last_name + " " + emp.first_name,
						  avatar = emp.avatar,
						  department = emp.department,
						  position = emp.position,
						  employee_id = emp.employee_id,
						  first_name = emp.first_name,
						  last_name = emp.last_name,
						  email_address = emp.email_address,
						  username = emp.username,
						  password = emp.password,
						  extension = emp.extension,
						  employee_asset = c.id,
					  }).ToList();


			return Json(db);

		}

		[HttpPost]
		public IActionResult Get_Return_Model(int employeeassetID)
		{


			var all_employee_asset = EA.GetAllEmployeeAssets_Admin_Default().Result;
			var db = all_employee_asset.Where(x => x.id == employeeassetID).FirstOrDefault();

			return Json(db);

		}

		public IActionResult Get_Employees_Asset_Model()
		{
			return Json(null);
		}

		public IActionResult Get_Return()
		{
			return Json(null);
		}

		public IActionResult Get_Admin_Admin()
		{
			var all_admin = Ad.GetAllAdmin().Result;

			return Json(all_admin);
		}

		public IActionResult Get_Application_Data_List()
		{
			var all_ea = EA.GetAllEmployeeAssets_Admin().Result;
			var all_it = IT_assets.GetAllITAssets_Admin().Result;
			var all_emp = Emp.GetAllEmployees_Admin().Result;

			var count = Application_Count();

			int no = 1;
			var db = (from c in all_ea
					  where c.leased.Contains("false")
					  join emp in all_emp on c.employee_id equals emp.employee_id
					  join it in all_it on c.asset_id equals it.asset_id
					  select new
					  {
						  no = no++,
						  date_out = c.date_out.ToString("yyyy-MM-dd"),
						  date_return = c.date_returned.ToString("yyyy-MM-dd"),
						  other_details = c.other_details,
						  assetImage = it.assetImage,
						  description = it.description,
						  name = emp.last_name + " " + emp.first_name,
						  department = emp.department,
						  employee_id = emp.employee_id,
						  employee_asset = c.id,
						  count = count,
					  }).ToList();



			return Json(db);
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


		//Form Data

		[HttpPost]
		public async Task<IActionResult> Form_Add_Employee(Employees emp)
		{
			Employees new_emp = new Employees()
			{
				first_name = emp.first_name,
				last_name = emp.last_name,
				department = emp.department,
				extension = emp.extension,
				email_address = emp.email_address,
				other_details = "none",
				avatar = "defaultAvatar.png",
				position = emp.position,
				username = emp.username,
				password = emp.password
			};

			await Emp.EmployeesCreateData_Admin(new_emp); // 等待异步操作完成

			return RedirectToAction("Employees_List", "Admin");
		}

		[HttpPost]
		public async Task<IActionResult> Form_Edit_Employee(Employees emp)
		{

			var data = Emp.GetAllEmployees_Admin().Result;
			var one = data.Where(x => x.employee_id == emp.employee_id).FirstOrDefault();

			Employees new_emp = new Employees()
			{
				employee_id = emp.employee_id,
				first_name = emp.first_name,
				last_name = emp.last_name,
				department = emp.department,
				extension = emp.extension,
				email_address = emp.email_address,
				other_details = "none",
				position = emp.position,
				username = emp.username,
				password = emp.password,
				avatar = one.avatar,
			};

			await Emp.EditEmployees_Admin(new_emp); // 等待异步操作完成

			return RedirectToAction("Employees_List", "Admin");
		}

		[HttpPost]
		public async Task<IActionResult> Form_Delete_Employee(Employees emp)
		{
			var data = await Emp.GetAllEmployees_Admin();
			var one = data.Where(x => x.employee_id == Convert.ToInt32(emp.employee_id)).FirstOrDefault();

			await Emp.DeleteEmployees_Admin(Convert.ToInt32(emp.employee_id));

			return RedirectToAction("Employees_List", "Admin");
		}

		[HttpPost]
		public async Task<IActionResult> Add_Asset_Form(IFormFile file, ITAssets it, int stock, string Inventoryotherdetails, IFormCollection formCollection)
		{
			List<string> selectedValues = formCollection["other_details"].ToList();
			string list_string = "";
			for (int index = 0; index < selectedValues.Count; index++)
			{
				string value = selectedValues[index];

				if (index != selectedValues.Count - 1)
				{
					list_string += value + "|";
				}
				else
				{
					list_string += value;
				}
			}

			var all_it = IT_assets.GetAllITAssets_Admin().Result;
			var last = all_it.LastOrDefault().asset_id + 1;

			string filePath = $@"wwwroot/Images/assets/{last + file.FileName}";

			ITAssets itasset = new ITAssets()
			{
				asset_type_code = it.asset_type_code,
				assetImage = last + file.FileName,
				description = it.description,
				other_details = list_string,
				status = true
			};

			await IT_assets.ITAssetsCreateData_Admin(itasset);
			var all_it_2 = IT_assets.GetAllITAssets_Admin().Result;
			var last2 = all_it_2.LastOrDefault().asset_id;
			DateTime currentDate = DateTime.Now;
			DateTime currentDateOnly = currentDate.Date;

			ITAssetInventory itassetinventory = new ITAssetInventory()
			{
				asset_id = last2,
				inventory_date = currentDateOnly,
				number_assigned = 0,
				number_in_stock = stock,
				other_details = Inventoryotherdetails,
			};

			IT_assetInven.ITAssetInventoryCreateData_Admin(itassetinventory);


			using (var stream = System.IO.File.Create(filePath))
			{
				await file.CopyToAsync(stream);
			};

			return RedirectToAction("Assets_List", "Admin");
		}

		[HttpPost]
		public async Task<IActionResult> Edit_Asset_Form(IFormFile file, ITAssets it, int stock, string asset_type_description, string Inventoryotherdetails)
		{

			var all_it = IT_assets.GetAllITAssets_Admin().Result;
			var one = all_it.Where(x => x.asset_id == it.asset_id).FirstOrDefault();
			var all_itinventory = IT_assetInven.GetAllITAssetInventory_Admin().Result;
			var one2 = all_itinventory.Where(x => x.asset_id == it.asset_id).FirstOrDefault();
			var all_ittype = AT.GetAllAssetTypes_Admin().Result;
			var one3 = all_ittype.Where(x => x.asset_type_code == one.asset_type_code).FirstOrDefault();


			if (file != null)
			{
				string filePath = $@"wwwroot/Images/assets/{file.FileName}";

				ITAssets itasset = new ITAssets()
				{
					asset_id = it.asset_id,
					asset_type_code = it.asset_type_code,
					assetImage = file.FileName,
					description = it.description,
					other_details = one.other_details,
					status = it.status
				};

				await IT_assets.EditITAssets_Admin(itasset);

				DateTime currentDate = DateTime.Now;
				DateTime currentDateOnly = currentDate.Date;

				ITAssetInventory itassetinventory = new ITAssetInventory()
				{
					it_asset_inventory_id = one2.it_asset_inventory_id,
					asset_id = one.asset_id,
					inventory_date = currentDateOnly,
					number_assigned = one2.number_assigned,
					number_in_stock = stock,
					other_details = Inventoryotherdetails,
				};

				await IT_assetInven.EditITAssetInventory_Admin(itassetinventory);

				using (var stream = System.IO.File.Create(filePath))
				{
					await file.CopyToAsync(stream);
				};
			}
			else
			{
				ITAssets itasset = new ITAssets()
				{
					asset_id = it.asset_id,
					asset_type_code = it.asset_type_code,
					assetImage = one.assetImage,
					description = it.description,
					other_details = one.other_details,
					status = it.status
				};

				await IT_assets.EditITAssets_Admin(itasset);

				DateTime currentDate = DateTime.Now;
				DateTime currentDateOnly = currentDate.Date;

				ITAssetInventory itassetinventory = new ITAssetInventory()
				{
					it_asset_inventory_id = one2.it_asset_inventory_id,
					asset_id = one.asset_id,
					inventory_date = currentDateOnly,
					number_assigned = one2.number_assigned,
					number_in_stock = stock,
					other_details = Inventoryotherdetails,
				};

				await IT_assetInven.EditITAssetInventory_Admin(itassetinventory);
			}

			return RedirectToAction("Assets_List", "Admin");
		}

		[HttpPost]
		public async Task<IActionResult> Delete_Asset_Form(int asset_id)
		{
			var all_it = IT_assets.GetAllITAssets_Admin().Result;
			var it_data = all_it.Where(x => x.asset_id == asset_id).FirstOrDefault();
			var it_inventory = IT_assetInven.GetAllITAssetInventory_Admin().Result;
			var it_data_inventory = it_inventory.Where(x => x.asset_id == asset_id).FirstOrDefault();

			await IT_assets.DeleteITAssets_Admin(it_data.asset_id);
			await IT_assetInven.DeleteITAssetInventory_Admin(it_data_inventory.it_asset_inventory_id);

			return RedirectToAction("Assets_List", "Admin");
		}

		[HttpPost]
		public async Task<IActionResult> Agree_Application(int employee_asset_id)
		{
			var adminValue = HttpContext.Session.GetString("Admin");
			var all_ea = EA.GetAllEmployeeAssets_Admin().Result;
			var ea_data = all_ea.Where(x => x.id == employee_asset_id).FirstOrDefault();
			ea_data.leased = "true";
			ea_data.admin = Convert.ToInt32(adminValue);

			await EA.EditEmployeeAssets_Admin(ea_data);

			var itin = IT_assetInven.GetAllITAssetInventory_Admin().Result;
			var data_itin = itin.Where(x => x.asset_id == ea_data.asset_id).FirstOrDefault();
			data_itin.number_in_stock -= 1;
			data_itin.number_assigned += 1;

			await IT_assetInven.EditITAssetInventory_Admin(data_itin);

			return Json(null);
		}

		[HttpPost]
		public async Task<IActionResult> Delete_Application(int employee_asset_id)
		{
			var adminValue = HttpContext.Session.GetString("Admin");
			var all_ea = EA.GetAllEmployeeAssets_Admin().Result;
			var ea_data = all_ea.Where(x => x.id == employee_asset_id).FirstOrDefault();
			ea_data.leased = "del";
			ea_data.admin = Convert.ToInt32(adminValue);

			await EA.EditEmployeeAssets_Admin(ea_data);

			return Json(null);
		}

		[HttpPost]
		public async Task<IActionResult> Appication_asset_emp(EmployeeAssets ea, int asset)
		{
			var all_it = IT_assets.GetAllITAssets().Result;
			var it_data = all_it.Where(x => x.asset_id == asset).FirstOrDefault();

			var empValue = HttpContext.Session.GetString("User");
			var all_emp = Emp.GetAllEmployees().Result;
			var one = all_emp.Where(x => x.employee_id == Convert.ToInt32(empValue)).FirstOrDefault();

			EmployeeAssets ea_data = new EmployeeAssets()
			{
				asset_id = it_data.asset_id,
				employee_id = one.employee_id,
				date_out = ea.date_out,
				date_returned = ea.date_returned,
				condition_out = "New",
				condition_returned = "none",
				other_details = ea.other_details,
				status = false,
				leased = "false",
				admin = 0,
			};

			await EA.EmployeeAssetsCreateData(ea_data);

			return RedirectToAction("HomePage", "Employee");
		}

		[HttpPost]
		public async Task<IActionResult> Edit_Employee_emp(IFormFile file, Employees emp)
		{

			var empValue = HttpContext.Session.GetString("User");
			var all_emp = Emp.GetAllEmployees().Result;
			var one = all_emp.Where(x => x.employee_id == Convert.ToInt32(empValue)).FirstOrDefault();


			if (file != null)
			{
				string filePath = $@"wwwroot/Images/avatar/{file.FileName}";

				one.avatar = file.FileName;
				one.first_name = emp.first_name;
				one.last_name = emp.last_name;
				one.extension = emp.extension;
				one.email_address = emp.email_address;
				one.username = emp.username;
				one.password = emp.password;

				await Emp.EditEmployees(one);

				using (var stream = System.IO.File.Create(filePath))
				{
					await file.CopyToAsync(stream);
				};
			}
			else
			{
				one.first_name = emp.first_name;
				one.last_name = emp.last_name;
				one.extension = emp.extension;
				one.email_address = emp.email_address;
				one.username = emp.username;
				one.password = emp.password;

				await Emp.EditEmployees(one);
			}

			return RedirectToAction("HomePage", "Employee");
		}

		[HttpPost]
		public async Task<IActionResult> ReturnAsset_Employee_emp(int id, string selectValue)
		{
			DateTime currentTime = DateTime.Now;

			var all_ea = EA.GetAllEmployeeAssets().Result;
			var one = all_ea.Where(x => x.id == id).FirstOrDefault();
			one.status = true;
			one.condition_returned = selectValue;
			one.date_returned = currentTime;

			//日期
			await EA.EditEmployeeAssets(one);

			var itin = IT_assetInven.GetAllITAssetInventory().Result;
			var data_itin = itin.Where(x => x.asset_id == one.asset_id).FirstOrDefault();
			data_itin.number_in_stock += 1;
			data_itin.number_assigned -= 1;

			await IT_assetInven.EditITAssetInventory(data_itin);

			return RedirectToAction("HomePage", "Employee");
		}

		[HttpPost]
		public async Task<IActionResult> Contact_Data(Contact ct)
		{
			Contact con = new Contact()
			{
				email = ct.email,
				name = ct.name,
				message = ct.message,
			};

			await Con.ContactsCreateData_Admin(con);

			return RedirectToAction("Contact", "Home");
		}


	}
}
