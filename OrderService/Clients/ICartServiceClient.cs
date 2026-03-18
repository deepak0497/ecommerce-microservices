using Shared.Contracts.Models;
using System.Threading.Tasks;

namespace OrderService.Clients;
    public interface ICartServiceClient
{

    Task<List<CartItem>> GetCart(string userId);
}