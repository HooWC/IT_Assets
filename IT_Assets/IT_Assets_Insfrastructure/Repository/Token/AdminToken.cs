using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT_Assets_DomainModelEntity.Models;
using Newtonsoft.Json;

namespace IT_Assets_Insfrastructure.Repository.Token
{
	public class AdminToken
	{
		public static string username = null;
		public static string password = null;

		public async Task<string> Token_Login()
		{
			using (var httpClient = new HttpClient())
			{
				Admin ad = new Admin()
				{
					username = username,
					password = password
				};

				string token = null;
				StringContent content = new StringContent(JsonConvert.SerializeObject(ad), Encoding.UTF8, "application/json");
				using (var response = await httpClient.PostAsync("https://localhost:7170/api/AdminToken", content))
				{
					token = await response.Content.ReadAsStringAsync();
				}
				return token;
			}
		}

		public async Task<string> Token()
		{
			using (var httpClient = new HttpClient())
			{
				Admin ad = new Admin()
				{
					username = "hoo",
					password = "hoo"
				};

				string token = null;
				StringContent content = new StringContent(JsonConvert.SerializeObject(ad), Encoding.UTF8, "application/json");
				using (var response = await httpClient.PostAsync("https://localhost:7170/api/AdminToken", content))
				{
					token = await response.Content.ReadAsStringAsync();
				}
				return token;
			}
		}
	}
}
