using BogusStore.Shared.Products;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BogusStore.Client.Products;

public class ProductService : IProductService
{
    private readonly HttpClient client;
    private const string endpoint = "api/product";
    public ProductService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<int> CreateAsync(ProductDto.Mutate request)
    {
        var response = await client.PostAsJsonAsync(endpoint,request);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task DeleteAsync(int productId)
    {
        await client.DeleteAsync($"{endpoint}/{productId}");
    }

    public async Task<ProductDto.Detail> GetDetailAsync(int productId)
    {
        var response = await client.GetFromJsonAsync<ProductDto.Detail>($"{endpoint}/{productId}");
        return response;
    }

    public async Task<ProductResult.Index> GetIndexAsync(ProductRequest.Index request)
    {
        // Currently not doing anything with the parameters from the request.
        var response = await client.GetFromJsonAsync<ProductResult.Index>($"{endpoint}?page=1&pagesize=10");
        return response;
    }

    public Task EditAsync(int productId, ProductDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task AddTagAsync(int productId, int tagId)
    {
        throw new NotImplementedException();
    }
}
