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
	public class ITAssets_Api
	{
		const string baseUrl = "https://localhost:7170/api/";
		HttpClient client = new HttpClient();
		public AdminToken ad_token = new AdminToken();
		public EmployeeToken cus_token = new EmployeeToken();

		public async Task<List<ITAssets>> GetAllITAssets_Admin_Default()
		{
			string accessToken = ad_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "ITAssets");
			return JsonConvert.DeserializeObject<List<ITAssets>>(jsonStr);
		}

		public async Task<List<ITAssets>> GetAllITAssets_Admin()
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "ITAssets");
			return JsonConvert.DeserializeObject<List<ITAssets>>(jsonStr);
		}

		public async Task<List<ITAssets>> GetAllITAssets()
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "ITAssets");
			return JsonConvert.DeserializeObject<List<ITAssets>>(jsonStr);
		}

		public async Task<ITAssets> FindITAssets_Admin(int id)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "ITAssets/" + id);
			return JsonConvert.DeserializeObject<ITAssets>(jsonStr);
		}

		public async Task<ITAssets> FindITAssets(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "ITAssets/" + id);
			return JsonConvert.DeserializeObject<ITAssets>(jsonStr);
		}

		public async Task EditITAssets_Admin(ITAssets emp)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "ITAssets/" + emp.asset_id, jsonStr);
		}

		public async void EditITAssets(ITAssets emp)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "ITAssets/" + emp.asset_id, jsonStr);
		}

		public async Task DeleteITAssets_Admin(int id)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			await client.DeleteAsync(baseUrl + "ITAssets/" + id);
		}

		public async Task ITAssetsCreateData_Admin(ITAssets cur)
		{
			string accessToken = ad_token.Token_Login().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cur), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "ITAssets", jsonStr);
		}
	}
}
