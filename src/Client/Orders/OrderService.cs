using BogusStore.Shared.Orders;
using System.Net.Http.Json;

namespace BogusStore.Client.Orders;

public class OrderService : IOrderService
{
    private readonly HttpClient client;

    public OrderService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<int> CreateAsync(OrderDto.Create model)
    {
        var response = await client.PostAsJsonAsync("api/customer/place-order", model);
        return await response.Content.ReadFromJsonAsync<int>();
    }
}
