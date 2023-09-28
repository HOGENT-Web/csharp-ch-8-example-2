using BogusStore.Shared.Orders;

namespace BogusStore.Shared.Orders
{
    public interface IOrderService
    {
        Task<int> CreateAsync(OrderDto.Create model);
    }
}