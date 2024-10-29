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
	public class Contact_Api
	{
		const string baseUrl = "https://localhost:7170/api/";
		HttpClient client = new HttpClient();
		public AdminToken ad_token = new AdminToken();

		public async Task<List<Contact>> GetAllContacts_Admin_Default()
		{
			string accessToken = ad_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "Contact");
			return JsonConvert.DeserializeObject<List<Contact>>(jsonStr);

		}

		public async Task ContactsCreateData_Admin(Contact con)
		{
			string accessToken = ad_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(con), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "Contact", jsonStr);
		}
	}
}
