using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IT_Assets_DomainModelEntity.Models;
using IT_Assets_Insfrastructure.Repository.Token;
using Newtonsoft.Json;

namespace IT_Assets_Insfrastructure.Repository.Api
{
	public class EmployeeAssets_Api
	{
		const string baseUrl = "https://localhost:7170/api/";
		HttpClient client = new HttpClient();
		public AdminToken ad_token = new AdminToken();
		public EmployeeToken cus_token = new EmployeeToken();

		public async Task<List<EmployeeAssets>> GetAllEmployeeAssets_Admin_Default()
		{
			string accessToken = ad_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "EmployeeAssets");
			return JsonConvert.DeserializeObject<List<EmployeeAssets>>(jsonStr);
		}

		public async Task<List<EmployeeAssets>> GetAllEmployeeAssets_Admin()
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "EmployeeAssets");
			return JsonConvert.DeserializeObject<List<EmployeeAssets>>(jsonStr);
		}

		public async Task<List<EmployeeAssets>> GetAllEmployeeAssets()
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "EmployeeAssets");
			return JsonConvert.DeserializeObject<List<EmployeeAssets>>(jsonStr);
		}

		public async Task<EmployeeAssets> FindEmployeeAssets_Admin(int id)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "EmployeeAssets/" + id);
			return JsonConvert.DeserializeObject<EmployeeAssets>(jsonStr);
		}

		public async Task<EmployeeAssets> FindEmployeeAssets(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "EmployeeAssets/" + id);
			return JsonConvert.DeserializeObject<EmployeeAssets>(jsonStr);
		}

		public async Task EditEmployeeAssets_Admin(EmployeeAssets emp)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "EmployeeAssets/" + emp.id, jsonStr);
		}

		public async Task EditEmployeeAssets(EmployeeAssets emp)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "EmployeeAssets/" + emp.id, jsonStr);
		}

		public void DeleteEmployeeAssets_Admin(int id)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			client.DeleteAsync(baseUrl + "EmployeeAssets/" + id);
		}

		public async Task EmployeeAssetsCreateData_Admin(EmployeeAssets cur)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cur), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "EmployeeAssets", jsonStr);
		}

		public async Task EmployeeAssetsCreateData(EmployeeAssets cur)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cur), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "EmployeeAssets", jsonStr);
		}
	}
}
