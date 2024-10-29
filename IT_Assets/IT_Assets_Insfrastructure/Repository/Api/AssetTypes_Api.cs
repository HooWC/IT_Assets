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
	public class AssetTypes_Api
	{
		const string baseUrl = "https://localhost:7170/api/";
		HttpClient client = new HttpClient();
		public AdminToken ad_token = new AdminToken();
		public EmployeeToken cus_token = new EmployeeToken();

		public async Task<List<AssetTypes>> GetAllAssetTypes_Admin_Default()
		{
			string accessToken = ad_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "AssetTypes");
			return JsonConvert.DeserializeObject<List<AssetTypes>>(jsonStr);
		}

		public async Task<List<AssetTypes>> GetAllAssetTypes_Admin()
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "AssetTypes");
			return JsonConvert.DeserializeObject<List<AssetTypes>>(jsonStr);
		}

		public async Task<List<AssetTypes>> GetAllAssetTypes()
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "AssetTypes");
			return JsonConvert.DeserializeObject<List<AssetTypes>>(jsonStr);
		}

		public async Task<AssetTypes> FindAssetTypes_Admin(int id)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "AssetTypes/" + id);
			return JsonConvert.DeserializeObject<AssetTypes>(jsonStr);
		}

		public async Task<AssetTypes> FindAssetTypes(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "AssetTypes/" + id);
			return JsonConvert.DeserializeObject<AssetTypes>(jsonStr);
		}

		public async void EditAssetTypes_Admin(AssetTypes emp)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "AssetTypes/" + emp.id, jsonStr);
		}

		public async void EditAssetTypes(AssetTypes emp)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "AssetTypes/" + emp.id, jsonStr);
		}

		public void DeleteAssetTypes_Admin(int id)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			client.DeleteAsync(baseUrl + "AssetTypes/" + id);
		}

		public async Task AssetTypesCreateData_Admin(AssetTypes cur)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cur), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "AssetTypes", jsonStr);
		}
	}
}
