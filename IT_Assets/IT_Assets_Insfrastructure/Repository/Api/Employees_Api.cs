using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IT_Assets_DomainModelEntity.Models;
using IT_Assets_Insfrastructure.Repository.Token;
using Newtonsoft.Json;

namespace IT_Assets_Insfrastructure.Repository.Api
{
	public class Employees_Api
	{

		//http://localhost:5198/api/
		const string baseUrl = "https://localhost:7170/api/";
		HttpClient client = new HttpClient();
		public AdminToken ad_token = new AdminToken();
		public EmployeeToken cus_token = new EmployeeToken();

		public async Task<List<Employees>> GetAllEmployees_Admin_Default()
		{
			string accessToken = ad_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "Employees");
			return JsonConvert.DeserializeObject<List<Employees>>(jsonStr);

		}

		public async Task<List<Employees>> GetAllEmployees_Admin()
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "Employees");
			return JsonConvert.DeserializeObject<List<Employees>>(jsonStr);
		}

		public async Task<List<Employees>> GetAllEmployees()
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "Employees");
			return JsonConvert.DeserializeObject<List<Employees>>(jsonStr);
		}

		public async Task<Employees> FindEmployees_Admin(int id)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "Employees/" + id);
			return JsonConvert.DeserializeObject<Employees>(jsonStr);
		}

		public async Task<Employees> FindEmployees(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "Employees/" + id);
			return JsonConvert.DeserializeObject<Employees>(jsonStr);
		}

		public async Task EditEmployees_Admin(Employees emp)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "Employees/" + emp.employee_id, jsonStr);
		}

		public async Task EditEmployees(Employees emp)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "Employees/" + emp.employee_id, jsonStr);
		}

		public async Task DeleteEmployees_Admin(int id)
		{
			string accessToken = await ad_token.Token_Login();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			await client.DeleteAsync(baseUrl + "Employees/" + id);
		}

		public async Task EmployeesCreateData_Admin(Employees cur)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cur), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "Employees", jsonStr);
		}
	}
}
