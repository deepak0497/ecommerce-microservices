using Shared.Contracts.DTO;

public interface IProductServiceClient
{
    Task<ProductDto> GetProduct(Guid productId);
}