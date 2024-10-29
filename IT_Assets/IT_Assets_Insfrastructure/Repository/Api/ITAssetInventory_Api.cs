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
	public class ITAssetInventory_Api
	{
		const string baseUrl = "https://localhost:7170/api/";
		HttpClient client = new HttpClient();
		public AdminToken ad_token = new AdminToken();
		public EmployeeToken cus_token = new EmployeeToken();

		public async Task<List<ITAssetInventory>> GetAllITAssetInventory_Admin_Default()
		{
			string accessToken = ad_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "ITAssetInventory");
			return JsonConvert.DeserializeObject<List<ITAssetInventory>>(jsonStr);
		}

		public async Task<List<ITAssetInventory>> GetAllITAssetInventory_Admin()
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "ITAssetInventory");
			return JsonConvert.DeserializeObject<List<ITAssetInventory>>(jsonStr);
		}

		public async Task<List<ITAssetInventory>> GetAllITAssetInventory()
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "ITAssetInventory");
			return JsonConvert.DeserializeObject<List<ITAssetInventory>>(jsonStr);
		}

		public async Task<ITAssetInventory> FindITAssetInventory_Admin(int id)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "ITAssetInventory/" + id);
			return JsonConvert.DeserializeObject<ITAssetInventory>(jsonStr);
		}

		public async Task<ITAssetInventory> FindITAssetInventory(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "ITAssetInventory/" + id);
			return JsonConvert.DeserializeObject<ITAssetInventory>(jsonStr);
		}

		public async Task EditITAssetInventory_Admin(ITAssetInventory emp)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "ITAssetInventory/" + emp.it_asset_inventory_id, jsonStr);
		}

		public async Task EditITAssetInventory(ITAssetInventory emp)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "ITAssetInventory/" + emp.it_asset_inventory_id, jsonStr);
		}

		public async Task DeleteITAssetInventory_Admin(int id)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			await client.DeleteAsync(baseUrl + "ITAssetInventory/" + id);
		}

		public async Task ITAssetInventoryCreateData_Admin(ITAssetInventory cur)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cur), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "ITAssetInventory", jsonStr);
		}
	}
}
