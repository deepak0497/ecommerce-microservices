using Newtonsoft.Json;
using Shared.Contracts.Models;
using System.Net.Http;

namespace OrderService.Clients;

public class CartServiceClient : ICartServiceClient
{

	private readonly HttpClient _httpClient;

	public CartServiceClient(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<CartItem>>  GetCart(string userId)
	{

		var response = await _httpClient.GetAsync($"/api/cart/{userId}");

		response.EnsureSuccessStatusCode();

		var content =await response.Content.ReadAsStringAsync();
		return JsonConvert.DeserializeObject<List<CartItem>>(content);
	}

   
}