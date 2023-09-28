using System;
using BogusStore.Domain.Customers;
using BogusStore.Domain.Orders;
using BogusStore.Domain.Products;
using BogusStore.Domain.Sessions;
using BogusStore.Persistence;
using BogusStore.Shared.Orders;
using Microsoft.EntityFrameworkCore;

namespace BogusStore.Services.Orders;

public class OrderService : IOrderService
{
    private readonly BogusDbContext dbContext;
    private readonly ISession session;

    public OrderService(BogusDbContext dbContext, ISession session)
    {
        this.dbContext = dbContext;
        this.session = session;
    }

    public async Task<int> CreateAsync(OrderDto.Create model)
    {
        Customer? customer = await dbContext.Customers.SingleOrDefaultAsync(x => x.Id == session.UserId);

        if (customer is null)
            throw new EntityNotFoundException(nameof(Customer), session.UserId!);

        IEnumerable<Product> products = await dbContext.Products
                                                .Where(x => model.Items.Select(x => x.ProductId).Contains(x.Id))
                                                .ToListAsync();

        List<OrderItem> orderItems = new();

        foreach (var item in model.Items)
        {
            Product? p = products.FirstOrDefault(x => x.Id == item.ProductId);

            if (p is null)
                throw new EntityNotFoundException(nameof(Product), item.ProductId);

            orderItems.Add(new OrderItem(p, item.Quantity));
        }

        Order order = customer.PlaceOrder(orderItems);
        await dbContext.SaveChangesAsync();

        return order.Id;
    }
}

