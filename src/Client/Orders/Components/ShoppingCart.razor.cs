using System;
using Append.Blazor.Sidepanel;
using BogusStore.Shared.Customers;
using BogusStore.Shared.Orders;
using Microsoft.AspNetCore.Components;

namespace BogusStore.Client.Orders.Components;

public partial class ShoppingCart : IDisposable
{
    [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
    [Inject] public Cart Cart { get; set; } = default!;
    [Inject] public IOrderService OrderService { get; set; } = default!;

    protected override void OnInitialized()
    {
        Cart.OnCartChanged += StateHasChanged;
    }

    public void Dispose()
    {
        Cart.OnCartChanged -= StateHasChanged;
    }

    public async Task PlaceOrderAsync()
    {
        OrderDto.Create request = new()
        {
            Items = Cart.Items.Select(x => new OrderItemDto.Create
            {
                ProductId = x.ProductId,
                Quantity = x.Amount
            })
        };

        await OrderService.CreateAsync(request);
        Cart.Clear();
        Sidepanel.Close();
    }
}

