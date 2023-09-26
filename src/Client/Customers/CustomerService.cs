using System;
using System.Net.Http.Json;
using BogusStore.Client.Extensions;
using BogusStore.Shared.Customers;
using BogusStore.Shared.Products;

namespace BogusStore.Client.Customers;

public class CustomerService : ICustomerService
{
    private readonly HttpClient client;
    private const string endpoint = "api/customer";
    public CustomerService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<CustomerResult.Index> GetIndexAsync(CustomerRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<CustomerResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }

    public Task<CustomerDto.Detail> GetDetailAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateAsync(CustomerDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(int customerId, CustomerDto.Mutate model)
    {
        throw new NotImplementedException();
    }
}

