using System;
using BogusStore.Shared.Customers;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Customers;

public partial class Detail
{
    [Inject] public ICustomerService CustomerService { get; set; } = default!;
    [Parameter] public int Id { get; set; }
    private CustomerDto.Detail? customer;

    protected override async Task OnInitializedAsync()
    {
        customer = await CustomerService.GetDetailAsync(Id);
    }
}

