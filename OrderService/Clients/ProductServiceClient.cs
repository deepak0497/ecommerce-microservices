using Shared.Contracts.DTO;
using System.Text.Json;

public class ProductServiceClient: IProductServiceClient
{
    private readonly HttpClient _httpClient;
    public ProductServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ProductDto> GetProduct(Guid productId)
    {
        var response = await _httpClient.GetAsync($"/api/products/{productId}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProductDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}