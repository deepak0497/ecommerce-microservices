using CartService.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace CartService.Services
{
    public class CartRedisService
    {

        private readonly IDatabase _database;

        public CartRedisService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task AddToCartAsync(string userId, CartItem item)
        {
            var key = $"cart:{userId}";
           var cart =await GetCart(userId);
            cart.Add(item);
            var json =JsonSerializer.Serialize(cart);

            await _database.StringSetAsync(key, json);
        }

        public async Task<List<CartItem>> GetCart(string userId)
        {
            var key = $"cart:{userId}";
            var data = await _database.StringGetAsync(key);
            if (data.IsNullOrEmpty)
                return new List<CartItem>();
            return JsonSerializer.Deserialize<List<CartItem>>(data);
        }
    }
}
