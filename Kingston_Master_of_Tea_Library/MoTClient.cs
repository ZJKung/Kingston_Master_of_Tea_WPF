using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Domain.Entities;

namespace Kingston_Master_of_Tea_Application
{
	public class MoTClient
	{
		private readonly HttpClient _client;

		public MoTClient(HttpClient client)
		{
			client.BaseAddress = new Uri("https://localhost:5001/");
			_client = client;
		}
		public async Task<List<BeverageItem>> GetBeverageItem()
		{
			
			return await _client.GetFromJsonAsync<List<BeverageItem>>("api/beverages");
		}

	}
}
